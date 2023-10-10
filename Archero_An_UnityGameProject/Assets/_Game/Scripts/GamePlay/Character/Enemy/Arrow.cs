using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : GameUnit
{
    public float speed = 10f;
    public Rigidbody rb;

    public void OnInit(Vector3 direct)
    {
        rb.velocity = speed * direct;
        TF.forward = direct;
        Invoke(nameof(OnDespawn), 5f);
    }


    public void OnDespawn()
    {
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_HERO))
        {
            other.GetComponent<Character>().OnHit(50f);
            OnDespawn();
        }
        if(other.CompareTag(Constant.TAG_WALL))
        {
            OnDespawn();
        }
    }
}
