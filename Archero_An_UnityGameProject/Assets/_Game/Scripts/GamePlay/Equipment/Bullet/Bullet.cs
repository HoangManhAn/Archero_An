using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameUnit
{ 

    public float speed = 500f;
    public Rigidbody rb;


    public void OnInit(Vector3 direct)
    {
        rb.velocity = speed * direct;
        TF.forward = direct;
        Invoke(nameof(OnDespawn), 5f);
    }


    public void OnDespawn()
    {
        //Destroy(gameObject);
        SimplePool.Despawn(this);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Character>().OnHit(100f);
            OnDespawn();
        }
        if (other.CompareTag("Wall"))
        {
            OnDespawn();
        }
    }
}
