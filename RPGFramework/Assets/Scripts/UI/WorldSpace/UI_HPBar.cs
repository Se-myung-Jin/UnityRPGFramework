using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar : UI_Base
{
    Stat _stat;

    enum GameObjects
    {
        HPBar
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));

        _stat = transform.parent.GetComponent<Stat>();
    }

    private void Update()
    {
        Transform parent = transform.parent;
        transform.position = parent.position + Vector3.up * (parent.GetComponent<Collider>().bounds.size.y);
        transform.rotation = Camera.main.transform.rotation;

        float ratio = _stat.HP / (float)_stat.MaxHp;
        SetHpRation(ratio);
    }

    public void SetHpRation(float ratio)
    {
        GetObject((int)GameObjects.HPBar).GetComponent<Slider>().value = ratio;
    }
}
