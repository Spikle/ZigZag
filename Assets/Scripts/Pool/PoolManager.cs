using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;

    public PoolPart[] pools;
    public void Awake()
    {
        Init();
        instance = this;
    }

    public void Init()
    {
        for (int i = 0; i < pools.Length; i++)
        {
            if (pools[i].prefab != null)
            {
                pools[i].pool = new ObjectPooling();
                pools[i].pool.Initialize(pools[i].count, pools[i].prefab, transform);
            }
        }
    }

    public GameObject GetObject(string name, Vector3 position, Quaternion rotation)
    {
        GameObject result = null;
        if (pools != null)
        {
            for (int i = 0; i < pools.Length; i++)
            {
                if (pools[i].name.Equals(name))
                { 
                    result = pools[i].pool.GetObject().gameObject;
                    result.transform.position = position;
                    result.transform.rotation = rotation;
                    result.SetActive(true);
                    return result;
                }
            }
        }
        return result;
    }

    public GameObject GetObject(string name, Transform parent)
    {
        GameObject go = GetObject(name, parent.position, parent.rotation);
        if (go != null)
            go.transform.SetParent(parent);
        return go;
    }

}

[System.Serializable]
public struct PoolPart
{
    public string name;
    public PoolObject prefab;
    public int count;
    public ObjectPooling pool;
}
