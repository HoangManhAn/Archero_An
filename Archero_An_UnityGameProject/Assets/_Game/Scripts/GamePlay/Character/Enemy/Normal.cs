using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normal : Minion
{
    

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
        hp = 200f;
        attackRange = 5f;
        GetHealthBar(hp);
        isCanMove = true;
        stateMachine.ChangeState(IdleState);
    }


    public void SetTarget(Hero hero)
    {
        this.target = hero;

        if (IsTargetInRange())
        {
            stateMachine.ChangeState(AttackState);
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

                if (IsTargetInRange())
                {
                    stateMachine.ChangeState(AttackState);
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
                    MoveAround(TF.position + 3f * RandomDirect());
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

    private void AttackState(ref Action onEnter, ref Action onExecute, ref Action onExit)
    {
        float timer = 0f;

        onEnter = () =>
        {


            if (Target != null)
            {
                //Doi huong enemy toi huong cua player
                ChangeDirect();

                ChangeAnim("attack");
                agent.speed = 10f;
                MoveToHero();
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
