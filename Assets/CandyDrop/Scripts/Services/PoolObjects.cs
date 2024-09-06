using System.Collections.Generic;
using UnityEngine;

public class PoolObjects<T> where T : MonoBehaviour
{
    protected static List<T> _objects = new List<T>();

    public static T GetObject(T prefabObject, Transform transformParent = null)
    {
        foreach (var currentObject in _objects)
        {
            if (currentObject != null && currentObject.gameObject != null && currentObject.gameObject.activeSelf == false)
            {
                currentObject.gameObject.SetActive(true);
                return currentObject;
            }
        }

        return AddObject(prefabObject, transformParent);
    }

    protected static T AddObject(T prefabObject, Transform transformParent = null)
    {
        T createdObject;
        if (transformParent != null)
            createdObject = UnityEngine.Object.Instantiate(prefabObject, transformParent);
        else
            createdObject = UnityEngine.Object.Instantiate(prefabObject);

        _objects.Add(createdObject);
        return createdObject;
    }

    public static void Clear()
    {
        foreach (var currentObject in _objects)
        {
            if (currentObject != null)
                GameObject.Destroy(currentObject.gameObject);
        }

        _objects.Clear();
    }
}
