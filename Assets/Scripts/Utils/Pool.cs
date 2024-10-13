using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Pool
{
    private GameObject prefab;
    private IObjectPool<GameObject> pool;
    Transform root;

    public Pool(GameObject prefab, Transform root)
    {
        this.prefab = prefab;
        this.root = root;

        pool = new ObjectPool<GameObject>(OnCreate, OnGet, OnRelease, OnDestroy);
    }
    public void Push(GameObject go)
    {
        pool.Release(go);
    }
    public GameObject Pop()
    {
        return pool.Get();
    }
    private GameObject OnCreate()
    {
        GameObject go = GameObject.Instantiate(prefab);
        go.transform.SetParent(root);
        go.name = prefab.name;
        return go;
    }
    private void OnGet(GameObject go)
    {
        go.SetActive(true);
    }
    private void OnRelease(GameObject go)
    {
        go.SetActive(false);
    }
    private void OnDestroy(GameObject go)
    {
        GameObject.Destroy(go);
    }
}
