using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcherAttackRange : MonoBehaviour
{
    public Acher acher;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hero"))
        {
            acher.SetTarget(other.GetComponent<Hero>());

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hero"))
        {
            acher.SetTarget(null);

        }
    }

}
