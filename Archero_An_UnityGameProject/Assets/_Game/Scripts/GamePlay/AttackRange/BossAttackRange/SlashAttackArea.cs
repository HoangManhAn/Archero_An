using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAttackArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hero"))
        {
            other.GetComponent<Character>().OnHit(100f);
        }
    }
}
