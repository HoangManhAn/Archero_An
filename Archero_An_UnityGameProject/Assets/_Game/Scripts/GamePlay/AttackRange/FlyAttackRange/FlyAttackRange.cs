using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAttackRange : MonoBehaviour
{
    public Fly fly;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_HERO))
        {
            fly.SetTarget(other.GetComponent<Hero>());

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constant.TAG_HERO))
        {
            fly.SetTarget(null);

        }
    }
}
