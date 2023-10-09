using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAttackRange : MonoBehaviour
{

    public Tank tank;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hero"))
        {
            tank.SetTarget(other.GetComponent<Hero>());
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hero"))
        {
            tank.SetTarget(null);

        }
    }


}
