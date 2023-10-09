using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Transform heroStartPoint;
    public Hero heroPrefab;



    public List<EnemyPointData> enemySpawn = new List<EnemyPointData>();
    public List<Enemy> enemies = new List<Enemy>();


    public void OnInit()
    {
        for (int i = 0; i < enemySpawn.Count; i++)
        {
            for (int j = 0; j < enemySpawn[i].enemySpawnPoints.Count; j++)
            {
                Enemy enemy = Instantiate(enemySpawn[i].enemyPrefab, enemySpawn[i].enemySpawnPoints[j].position, Quaternion.identity);
                enemy.OnInit();
                enemies.Add(enemy);
            }
        }
    }

    public void ClearEnemy()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            Destroy(enemies[i].gameObject);
            Destroy(enemies[i].healthBar.gameObject);
        }
        enemies.Clear();
    }

    public void EnemyDead(Enemy enemy)
    {
        enemies.Remove(enemy);
    }
}

