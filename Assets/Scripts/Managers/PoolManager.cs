using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    public event Action<GameObject> OnEnemyDespawn;

    Dictionary<Defines.EPoolTarget, Pool> pools = new Dictionary<Defines.EPoolTarget, Pool>();
    Dictionary<Defines.EPoolTarget, Transform> roots = new Dictionary<Defines.EPoolTarget, Transform>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public GameObject Spawn(Defines.EPoolTarget target, Transform parent = null)
    {
        if (pools.TryGetValue(target, out Pool pool) == false)
        {
            GameObject prefab = Resources.Load<GameObject>($"Prefabs/{target.ToString()}");
            pool = new Pool(prefab, parent);
            pools.Add(target, pool);
        }

        return pool.Pop();
    }

    public Transform PoolRoot(Defines.EPoolTarget target)
    {
        if (roots.TryGetValue(target, out Transform root) == false)
        {
            root = new GameObject($"{target}Root").transform;
            roots.Add(target, root);
        }

        return root;
    }

    public void Despawn(Defines.EPoolTarget target, GameObject obj)
    {
        if (pools.ContainsKey(target) == false)
        {
            Destroy(obj);
            return;
        }
        pools[target].Push(obj);

        if (Defines.EPoolTarget.Enemy == target)
            OnEnemyDespawn?.Invoke(obj);
    }

    public void NpcRegen(int count = 1)
    {
        Transform root = PoolRoot(Defines.EPoolTarget.Npc);

        for (int i = 0; i < count; i++)
        {
            GameObject go = Spawn(Defines.EPoolTarget.Npc, root);
            go.name = $"Npc_{i + 1}";
            go.transform.position = new Vector3(UnityEngine.Random.Range(-10, 10), 0, UnityEngine.Random.Range(-10, 10));
            NpcController npc = go.GetComponent<NpcController>();
            npc.SetCharacterName($"Npc_{i}");
        }
    }
}
