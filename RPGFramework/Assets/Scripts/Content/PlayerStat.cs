using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    [SerializeField]
    int _exp;
    [SerializeField]
    int _gold;

    public int Exp 
    { 
        get { return _exp; } 
        set 
        { 
            _exp = value;

            int level = Level;
            while (true)
            {
                if (GeneralManager.Data.StatDic.TryGetValue(level + 1, out Data.Stat stat) == false)
                    break;

                if (_exp < stat.totalExp)
                    break;

                level++;
            }

            if (level != Level)
            {
                Debug.Log("Level Up!!");
                Level = level;
                SetStat(Level);
            }
        }
    }
    public int gold { get { return _gold; } set { _gold = value; } }

    private void Start()
    {
        _level = 1;

        SetStat(_level);

        _exp = 0;
        _defense = 5;
        _moveSpeed = 5.0f;
        _gold = 0;
    }

    public void SetStat(int level)
    {
        Dictionary<int, Data.Stat> dic = GeneralManager.Data.StatDic;
        Data.Stat stat = dic[level];

        _hp = stat.maxHp;
        _maxHp = stat.maxHp;
        _attack = stat.attack;
    }

    public override void OnDead(Stat attacker)
    {
        
    }
}
