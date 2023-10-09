using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlay : UICanvas
{
    public TMP_Text level;

    public override void Open()
    {
        base.Open();
        CameraFollowNewVersion.Ins.ChangeState(CameraFollowNewVersion.State.Gameplay);
    }
    public void SettingButton()
    {
        UIManager.Ins.OpenUI<Setting>();
        GameManager.Ins.ChangeState(GameState.Pause);
        Close(0);
    }

    
}
