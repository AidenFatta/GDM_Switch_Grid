using UnityEngine;
using System.Collections.Generic;

public class ObjectPool
{
    private GameObject prefab;
    private List<GameObject> availableObjects;

    public ObjectPool(GameObject prefab, int initialSize)
    {
        this.prefab = prefab;
        availableObjects = new List<GameObject>();

        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = GameObject.Instantiate(prefab);
            obj.SetActive(false);
            availableObjects.Add(obj);
        }
    }

    public GameObject Get()
    {
        foreach (GameObject obj in availableObjects)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        GameObject newObj = GameObject.Instantiate(prefab);
        availableObjects.Add(newObj);
        return newObj;
    }

    public void Return(GameObject obj)
    {
        obj.SetActive(false);
    }
}
