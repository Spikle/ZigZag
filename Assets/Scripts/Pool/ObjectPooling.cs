using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling
{
    List<PoolObject> objects;
    Transform objectsParent;

    public void Initialize(int count, PoolObject obj, Transform parent)
    {
        objects = new List<PoolObject>();
        objectsParent = parent;
        for (int i = 0; i < count; i++)
        {
            AddObject(obj, parent);
        }
    }

    void AddObject(PoolObject obj, Transform parent)
    {
        GameObject go = GameObject.Instantiate(obj.gameObject);
        go.name = obj.name;
        go.transform.SetParent(parent);
        objects.Add(go.GetComponent<PoolObject>());
        go.SetActive(false);
    }

    public PoolObject GetObject()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i].gameObject.activeInHierarchy == false)
            {
                return objects[i];
            }
        }
        AddObject(objects[0], objectsParent);
        return objects[objects.Count - 1];
    }

}
