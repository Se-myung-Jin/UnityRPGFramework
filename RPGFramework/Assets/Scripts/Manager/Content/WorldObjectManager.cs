using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObjectManager
{
    GameObject _player;
    HashSet<GameObject> _monsters = new HashSet<GameObject>();

    public GameObject GetPlayer() { return _player; }

    public Action<int> OnSpawnEvent;

    public GameObject Spawn(Define.WorldObject type, string path, Transform parent = null)
    {
        GameObject obj = GeneralManager.Resource.Instanciate(path, parent);

        switch (type)
        {
            case Define.WorldObject.Monster:
                _monsters.Add(obj);
                if (OnSpawnEvent != null)
                    OnSpawnEvent.Invoke(1);
                break;
            case Define.WorldObject.Player:
                _player = obj;
                break;
        }

        return obj;
    }

    public Define.WorldObject GetWorldObjectType(GameObject obj)
    {
        BaseController controller = obj.GetComponent<BaseController>();
        if (controller == null)
            return Define.WorldObject.Unknown;

        return controller.WorldObjectType;
    }

    public void Despawn(GameObject obj)
    {
        Define.WorldObject type = GetWorldObjectType(obj);

        switch (type)
        {
            case Define.WorldObject.Monster:
                {
                    if (_monsters.Contains(obj))
                    {
                        _monsters.Remove(obj);
                        if (OnSpawnEvent != null)
                            OnSpawnEvent.Invoke(-1);
                    }
                }
                break;
            case Define.WorldObject.Player:
                {
                    if (_player == obj)
                        _player = null;
                }
                break;
        }

        GeneralManager.Resource.Destroy(obj);
    }
}
