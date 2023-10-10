using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : Enemy
{
    public bool IsTargetInRange()
    {
        if (target != null && Vector3.Distance(target.transform.position, transform.position) <= attackRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_HERO))
        {
            other.GetComponent<Character>().OnHit(30f);
        }
    }
}
