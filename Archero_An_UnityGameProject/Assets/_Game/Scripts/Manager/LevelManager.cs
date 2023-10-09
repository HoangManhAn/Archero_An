using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public List<Level> levels = new List<Level>();
    public Hero hero;
    public Level currentLevel;

    public int level = 1;

    private void Start()
    {
        UIManager.Ins.OpenUI<MainMenu>();
    }

    public void OnStart()
    {
        if(currentLevel != null)
        {
            ClearDataLevel();
        }
        level = 1;
        LoadLevel();
        
    }

    public void OnFinish()
    {
        currentLevel.ClearEnemy();
        UIManager.Ins.OpenUI<Lose>() ;
        GameManager.Ins.ChangeState(GameState.Finish);
    }

    public void LoadLevel()
    {
        UIManager.Ins.OpenUI<GamePlay>().level.text = "Level " + level.ToString();
        LoadLevel(level);
        OnInit();
    }
    public void LoadLevel(int indexLevel)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        currentLevel = Instantiate(levels[indexLevel - 1]);
    }

    public void OnInit()
    {
        currentLevel.OnInit();
        hero.transform.position = currentLevel.heroStartPoint.position;
        
        if (level == 1)
        {
            hero.OnInit();
        }
    }

    public void NextLevel()
    {
        level++;
        LoadLevel();
    }

    public void RestartGame()
    {
        level = 1;
        LoadLevel();
    }

    public void ClearDataLevel()
    {
        currentLevel.ClearEnemy();
        hero.targets.Clear();
    }
  

    
    
}
