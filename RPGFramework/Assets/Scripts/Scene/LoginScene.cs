using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginScene : BaseScene
{
    public override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Login;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            GeneralManager.Scene.LoadScene(Define.Scene.Game);
        }
    }

    public override void Clear()
    {

    }
}
