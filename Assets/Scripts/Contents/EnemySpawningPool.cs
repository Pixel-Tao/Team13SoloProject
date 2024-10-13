using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawningPool : MonoBehaviour
{
    [SerializeField] private int maxPoolSize = 5;
    [SerializeField] private float spawnTime = 3;
    [SerializeField] private float spawnRange = 5;

    private HashSet<GameObject> pools = new HashSet<GameObject>();
    private Transform parent;

    private void Awake()
    {
    }

    private void Start()
    {
        PoolManager.Instance.OnEnemyDespawn += EnemyDespawn;
        parent = PoolManager.Instance.PoolRoot(Defines.EPoolTarget.Enemy);
        StartCoroutine(CoSapwn());
    }

    private void EnemyDespawn(GameObject enemy)
    {
        if (pools.Contains(enemy))
            pools.Remove(enemy);
    }

    private IEnumerator CoSapwn()
    {
        while (true)
        {
            if (pools.Count < maxPoolSize)
            {
                Spawn(RandomPosition());
            }

            yield return new WaitForSeconds(spawnTime);
        }
    }

    private Vector2 RandomPosition()
    {
        float x = Random.Range(-spawnRange, spawnRange);
        float y = Random.Range(-spawnRange, spawnRange);

        return new Vector2(transform.position.x + x, transform.position.y + y);
    }

    private void Spawn(Vector2 position)
    {
        GameObject go = PoolManager.Instance.Spawn(Defines.EPoolTarget.Enemy, parent);
        EnemyController enemy = go.GetComponent<EnemyController>();
        go.transform.position = position;
        enemy.Init();
        pools.Add(go);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRange);
    }
}