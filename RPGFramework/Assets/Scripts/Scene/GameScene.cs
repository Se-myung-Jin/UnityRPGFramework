using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    public override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        GeneralManager.UI.ShowSceneUI<UI_Inven>();

        gameObject.GetOrAddComponent<CursorController>();
    }

    public override void Clear()
    {
        
    }
}
