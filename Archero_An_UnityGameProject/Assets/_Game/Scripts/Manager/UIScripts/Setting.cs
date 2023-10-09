using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : UICanvas
{
    public void ContinueButton()
    {
        UIManager.Ins.OpenUI<GamePlay>();
        GameManager.Ins.ChangeState(GameState.GamePlay);
        Close(0);
    }

    public void ResartButton()
    {
        UIManager.Ins.OpenUI<GamePlay>();
        GameManager.Ins.ChangeState(GameState.GamePlay);
        LevelManager.Ins.ClearDataLevel();
        LevelManager.Ins.RestartGame();
        Close(0);
    }

    public void MainMenuButton()
    {
        UIManager.Ins.OpenUI<MainMenu>();
        GameManager.Ins.ChangeState(GameState.MainMenu);
        Close(0);
    }
}
