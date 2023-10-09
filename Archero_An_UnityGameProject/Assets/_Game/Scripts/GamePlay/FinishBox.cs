using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FinishBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hero") && LevelManager.Ins.currentLevel.enemies.Count == 0)
        {
            LevelManager.Ins.NextLevel();
        }
    }
}
