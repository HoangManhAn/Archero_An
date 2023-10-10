using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeckAttackArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_HERO))
        {
            other.GetComponent<Character>().OnHit(50f);
        }
    }
}
