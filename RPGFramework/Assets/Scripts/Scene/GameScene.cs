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

        GameObject player = GeneralManager.Object.Spawn(Define.WorldObject.Player, "UnityChan");
        Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);

        GeneralManager.Object.Spawn(Define.WorldObject.Monster, "Knight");
    }

    public override void Clear()
    {
        
    }
}
