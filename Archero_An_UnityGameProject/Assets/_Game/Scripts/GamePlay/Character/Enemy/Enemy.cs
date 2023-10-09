using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using System;
using static UnityEngine.EventSystems.EventTrigger;


public class Enemy : Character
{

    public StateMachine stateMachine = new StateMachine();

    public LayerMask ground;


    [Header("-------------Enemy Movement-------------")]
    //cache transform de toi uu hieu nang
    //[SerializeField] protected Transform tf;
    [SerializeField] protected NavMeshAgent agent;

    //luu diem muc tieu se di den
    private Vector3 destination;

    protected float attackRange = 10f;

    protected bool isCanMove = true;

    //property tra ve ket qua xem la da toi diem muc tieu hay chua
    public bool IsDestionation => Vector3.Distance(TF.position, destination + (TF.position.y - destination.y) * Vector3.up) < 0.1f;

    protected Hero target;
    public Hero Target => target;

    public override void OnDespawn()
    {
        base.OnDespawn();
        Destroy(gameObject);
        Destroy(healthBar.gameObject);
        
        LevelManager.Ins.currentLevel.EnemyDead(this);
    }

    public override void OnDeath()
    {
        base.OnDeath();
        LevelManager.Ins.hero.targets.Remove(this);
        OnStopMove();
        ChangeAnim("die");
        Invoke(nameof(OnDespawn), 1f);
    }

    public override void OnStopMove()
    {
        SetDestination(TF.position);
        isCanMove = true;
    }

   

    //set diem den
    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        agent.SetDestination(destination);
    }

    public void MoveAround(Vector3 des)
    {
        if (CheckGround(des))
        {
            if (isCanMove)
            {
                SetDestination(des);
                isCanMove = false;
            }
            if (IsDestionation) { isCanMove = true; }
        }
    }

    public void MoveToHero()
    {
        SetDestination(this.target.TF.position);
    }

    public Vector3 RandomDirect()
    {
        int random = (int)UnityEngine.Random.Range(0f, 4f);

        Vector3 direct = Vector3.zero;

        switch (random)
        {
            case 0:
                direct = Vector3.zero;
                break;

            case 1:
                direct = Vector3.forward;
                break;

            case 2:
                direct = Vector3.back;
                break;

            case 3:
                direct = Vector3.right;
                break;

            case 4:
                direct = Vector3.left;
                break;

            default:
                break;

        }


        return direct;
    }

    public void ChangeDirect()
    {
        Vector3 direct = target.TF.position - TF.position;
        TF.forward = direct.normalized;
    }

    

    public bool CheckGround(Vector3 des)
    {
        RaycastHit hit;
        if (Physics.Raycast(des + Vector3.up * 2, Vector3.down, out hit, 10f, ground))
        {
            return true;
        }
        else
        {
            return false;
        }
    }



    



}
