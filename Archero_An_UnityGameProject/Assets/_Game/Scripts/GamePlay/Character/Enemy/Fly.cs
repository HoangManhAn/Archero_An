using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Fly : Minion
{
    

    public GameObject peckAttackArea;

    public float speed = 5f;
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
        GetHealthBar(hp);
        attackRange = 3f;
        isCanMove = true;
        stateMachine.ChangeState(IdleState);
    }

    public void FlyToHero()
    {

        //Doi huong enemy toi huong cua player
        ChangeDirect();
        TF.position = Vector3.MoveTowards(TF.position, target.transform.position, 10f * Time.deltaTime);

    }

    //public void FlyAround()
    //{
    //    Vector3 nextPos = tf.position + 5f * RandomDirect();
    //    if (CheckGround(nextPos))
    //    {
    //        if (isCanMove)
    //        {
    //            //transform.forward = (nextPos - tf.position);
    //            isCanMove = false;
    //            tf.position = Vector3.MoveTowards(tf.position, nextPos, 100f * Time.deltaTime);

    //        }
    //        if (Vector3.Distance(tf.position, nextPos) < 0.01f) { isCanMove = true; }
    //    }
    //}



    public override void OnStopMove()
    {
        //
    }

    public override void ResetAttack()
    {
        base.ResetAttack();
        peckAttackArea.SetActive(false);
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
            randomTime = UnityEngine.Random.Range(2f, 4f);
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
                    ChangeAnim(Constant.ANIM_RUN);
                    FlyToHero();
                }
            }
            else
            {
                if (timer < randomTime)
                {

                    ChangeAnim(Constant.ANIM_RUN);
                    //FlyAround();
                    FlyToHero();
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

                if (!isAttack)
                {
                    isAttack = true;

                    //Doi huong enemy toi huong cua player
                    ChangeDirect();

                    peckAttackArea.SetActive(true);
                    ChangeAnim(Constant.ANIM_ATTACK);
                    Invoke(nameof(ResetAttack), 1f);
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

