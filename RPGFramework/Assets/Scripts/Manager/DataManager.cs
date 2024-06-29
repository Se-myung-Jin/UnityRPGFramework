using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDic();
}

public class DataManager
{
    public Dictionary<int, Stat> StatDic { get; private set; } = new Dictionary<int, Stat>();

    public void Init()
    {
        StatDic = LoadJson<StatData, int, Stat>("StatData").MakeDic();
    }

    T LoadJson<T, Key, Value>(string path) where T : ILoader<Key, Value>
    {
        TextAsset asset = GeneralManager.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<T>(asset.text);
    }
}
