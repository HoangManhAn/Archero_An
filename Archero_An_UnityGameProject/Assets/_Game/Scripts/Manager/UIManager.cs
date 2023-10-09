//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;


//public class UIManager : Singleton<UIManager>
//{
//    public GameObject mainMenuUI;
//    public GameObject finishUI;
//    public GameObject gamePlayUI;
//    public GameObject settingUI;

//    public Text currentlevel;

//    public void OpenMainMenuUI()
//    {
//        mainMenuUI.SetActive(true);
//        finishUI.SetActive(false);
//        gamePlayUI.SetActive(false);
//        settingUI.SetActive(false);

        
//    }

//    public void OpenFinishUI()
//    {
//        mainMenuUI.SetActive(false);
//        finishUI.SetActive(true);
//        gamePlayUI.SetActive(false);
//        settingUI.SetActive(false);
//    }

//    public void OpenGamePlayUI()
//    {
//        mainMenuUI.SetActive(false);
//        gamePlayUI.SetActive(true);
//        finishUI.SetActive(false);
//        settingUI.SetActive(false);
//    }

//    public void OpenSettingUI()
//    {
//        mainMenuUI.SetActive(false);
//        gamePlayUI.SetActive(false);
//        finishUI.SetActive(false);
//        settingUI.SetActive(true);
//    }


//    public void StartButton()
//    {
//        OpenGamePlayUI();
//        LevelManager.Ins.OnStart();
//    }

//    public void RestartButton()
//    {
//        OpenGamePlayUI();
//        GameManager.Ins.ChangeState(GameState.GamePlay);
//        LevelManager.Ins.ClearDataLevel();
//        LevelManager.Ins.RestartGame();
//    }

//    public void SetttingButton()
//    {
//        OpenSettingUI();
//        GameManager.Ins.ChangeState(GameState.Pause);
//    }

//    public void MainMenuButton()
//    {
//        OpenMainMenuUI();
//        GameManager.Ins.ChangeState(GameState.MainMenu);
//    }

//    public void BackButton()
//    {
//        OpenGamePlayUI();
//        GameManager.Ins.ChangeState(GameState.GamePlay);
//    }


//}


