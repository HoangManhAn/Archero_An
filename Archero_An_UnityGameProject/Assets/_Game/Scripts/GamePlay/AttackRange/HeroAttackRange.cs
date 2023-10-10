using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttackRange : MonoBehaviour
{
    public Hero hero;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_ENEMY))
        {
            hero.targets.Add(other.GetComponent<Enemy>());
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constant.TAG_ENEMY))
        {
            hero.targets.Remove(other.GetComponent<Enemy>());
        }
    }



}
