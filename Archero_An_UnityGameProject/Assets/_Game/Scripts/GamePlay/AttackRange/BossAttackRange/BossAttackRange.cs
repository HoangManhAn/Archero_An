using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackRange : MonoBehaviour
{
    public Boss boss;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_HERO))
        {
            boss.SetTarget(other.GetComponent<Hero>());

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constant.TAG_HERO))
        {
            boss.SetTarget(null);

        }
    }
}
