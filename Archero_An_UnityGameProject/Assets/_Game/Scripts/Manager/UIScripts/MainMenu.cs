using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UICanvas
{
    public override void Open()
    {
        base.Open();
        CameraFollowNewVersion.Ins.ChangeState(CameraFollowNewVersion.State.MainMenu);
    }
    public void StartButton()
    {
        UIManager.Ins.OpenUI<GamePlay>();
        GameManager.Ins.ChangeState(GameState.GamePlay);
        LevelManager.Ins.OnStart();
        Close(0);
    }

    public void ShopButton()
    {
        UIManager.Ins.OpenUI<Shop>();
        Close(0);
    }
}
