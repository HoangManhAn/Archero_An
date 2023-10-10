using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttackRange : MonoBehaviour
{

    public Normal normal;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_ENEMY))
        {
            normal.SetTarget(other.GetComponent<Hero>());
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constant.TAG_ENEMY))
        {
            normal.SetTarget(null);

        }
    }


}
