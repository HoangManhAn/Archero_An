using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;


public enum GameState { MainMenu, GamePlay, Pause, Finish }

public class GameManager : Singleton<GameManager>
{
    
    private static GameState gameState = GameState.MainMenu;

    
    protected void Awake()
    {
        //base.Awake();
        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }

        

        ChangeState(GameState.MainMenu);
        UIManager.Ins.OpenUI<MainMenu>();
        LevelManager.Ins.hero.joyStick.gameObject.SetActive(false);
    }

    private void Start()
    {
        if(!PlayerPrefs.HasKey(PrefConst.COIN_KEY))
            Pref.Coins = 10000;

        GUIManager.Ins.UpdateCoins();
    }

    public void ChangeState(GameState state)
    {
        gameState = state;
    }

    public bool IsState(GameState state)
    {
        return gameState == state;
    }

}
