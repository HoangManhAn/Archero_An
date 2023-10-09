using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AttackType { None, RollAtack, SlashAttack }

public class Boss : Enemy
{

    public GameObject slashAttackArea;

    //private StateMachine stateMachine = new StateMachine();


    protected float rollAttackRange = 20f;
    protected float slashAttackRange = 5f;

    protected bool isCanRoll;

    private void Update()
    {
        if (GameManager.Ins.IsState(GameState.GamePlay))
        {
            if (!IsDead)
            {
                stateMachine?.Execute();
            }
            else
            {
                stateMachine.ChangeState(PauseState);
            }
        }
        else
        {
            stateMachine.ChangeState(PauseState);
        }

    }

    public override void OnInit()
    {
        base.OnInit();
        hp = 5000f;
        attackRange = 50f;
        GetHealthBar(5000f);
        isCanRoll = true;
        isCanMove = true;
        stateMachine.ChangeState(IdleState);
    }

    public void SetTarget(Hero hero)
    {
        this.target = hero;

        if (IsTargetInRange() != AttackType.None)
        {
            if(IsTargetInRange() == AttackType.RollAtack)
                stateMachine.ChangeState(RollAttackState);
            else
                stateMachine.ChangeState(SlashAttackState);
        }
        else if (Target != null)
        {
            stateMachine.ChangeState(PatrolState);
        }
        else
        {
            stateMachine.ChangeState(IdleState);
        }
    }

    public AttackType IsTargetInRange()
    {
        if (target != null)
        {
            float dis = Vector3.Distance(target.transform.position, transform.position);
            if (dis > slashAttackRange && dis <= rollAttackRange)
            {
                return AttackType.RollAtack;
            }
            else if (dis >= 0f && dis <= slashAttackRange)
            {
                return AttackType.SlashAttack;
            }
            else
            {
                return AttackType.None;
            }

        }
        else
        { return AttackType.None; }
    }

    public override void ResetAttack()
    {
        base.ResetAttack(); 
        slashAttackArea.SetActive(false);
        
    }

    public void RollMove()
    {
        
            if (isCanRoll)
            {
                SetDestination(target.transform.position);
                isCanMove = false;
            }
            if (Vector3.Distance(target.transform.position, TF.position) <= 3) { isCanRoll = true; }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hero"))
        {
            //Debug.Log(50);
            other.GetComponent<Character>().OnHit(50f);
        }
    }

    private void IdleState(ref Action onEnter, ref Action onExecute, ref Action onExit)
    {
        float timer = 0f;
        float randomTime = 0f;

        onEnter = () =>
        {
            timer = 0;
            randomTime = UnityEngine.Random.Range(3f, 4f);

            OnStopMove();
            ChangeAnim("idle");
        };

        onExecute = () =>
        {
            timer += Time.deltaTime;

            if (timer < randomTime)
            {
                stateMachine.ChangeState(PatrolState);
            }
        };

        onExit = () =>
        {

        };
    }

    private void PatrolState(ref Action onEnter, ref Action onExecute, ref Action onExit)
    {
        float timer = 0f;
        float randomTime = 0f;

        onEnter = () =>
        {
            timer = 0;
            randomTime = UnityEngine.Random.Range(2f, 4f);
        };

        onExecute = () =>
        {
            timer += Time.deltaTime;

            if (Target != null)
            {

                if (IsTargetInRange() == AttackType.RollAtack)
                {
                    stateMachine.ChangeState(RollAttackState);
                }
                else if (IsTargetInRange() == AttackType.SlashAttack)
                {
                    stateMachine.ChangeState(SlashAttackState);
                }
                else
                {
                    agent.speed = 5f;
                    ChangeAnim("run");
                    MoveToHero();
                }
            }
            else
            {
                if (timer < randomTime)
                {
                    agent.speed = 3f;
                    ChangeAnim("run");
                    MoveAround(TF.position + 5f * RandomDirect());
                }
                else
                {
                    stateMachine.ChangeState(IdleState);
                }
            }
        };

        onExit = () =>
        {

        };
    }

    private void RollAttackState(ref Action onEnter, ref Action onExecute, ref Action onExit)
    {
        float timer = 0f;

        onEnter = () =>
        {
            if (Target != null)
            {
                //Doi huong enemy toi huong cua player
                ChangeDirect();

                ChangeAnim("roll");
                agent.speed = 50f;
                //MoveToHero();
                RollMove();
            }

            timer = 0f;
        };

        onExecute = () =>
        {
            timer += Time.deltaTime;

            if (timer >= 1.5f)
            {
                stateMachine.ChangeState(PatrolState);
            }
        };

        onExit = () =>
        {

        };
    }

    private void SlashAttackState(ref Action onEnter, ref Action onExecute, ref Action onExit)
    {
        float timer = 0f;

        onEnter = () =>
        {
            if (Target != null)
            {
                //Doi huong enemy toi huong cua player
                ChangeDirect();

                if(!isAttack)
                {
                    OnStopMove();
                    slashAttackArea.SetActive(true);
                    ChangeAnim("slash");
                    Invoke(nameof(ResetAttack), 0.4f);
                }

            }

            timer = 0f;
        };

        onExecute = () =>
        {
            timer += Time.deltaTime;

            if (timer >= 1.5f)
            {
                stateMachine.ChangeState(PatrolState);
            }
        };

        onExit = () =>
        {

        };
    }

    private void PauseState(ref Action onEnter, ref Action onExecute, ref Action onExit)
    {

        onEnter = () =>
        {
            OnStopMove();
        };

        onExecute = () =>
        {

            if (!GameManager.Ins.IsState(GameState.Pause))
            {
                stateMachine.ChangeState(PatrolState);
            }

        };

        onExit = () =>
        {

        };
    }
}
