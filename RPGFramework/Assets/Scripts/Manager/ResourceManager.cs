using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public GameObject Instanciate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        if (prefab == null) return null;

        return Object.Instantiate(prefab, parent);
    }

    public void Destroy(GameObject go)
    {
        if (go == null) return;

        Object.Destroy(go);
    }

    public void Destroy(GameObject go, float timeSec)
    {
        if (go == null) return;

        Object.Destroy(go, timeSec);
    }
}
