using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : GameUnit
{
    public Transform skin;

    [Header("----------------------------------------")]
    [SerializeField] protected HealthBar healthBarPrefab;
    public HealthBar healthBar;

    [SerializeField] protected CombatText combatTextPrefab;

    [Header("-------------Anim-------------")]
    [SerializeField] private Animator anim;
    protected string currentAnimName;

    protected float hp;

    protected bool isAttack = false;
    public bool IsDead => hp <= 0;



    public virtual void OnInit()
    {
        //For override
    }

    public virtual void OnDespawn()
    {
        //For override
    }

    public virtual void OnDeath()
    {
        //For override
    }

    public virtual void OnStopMove()
    {
        //For override
    }
    public virtual void OnAttack()
    {
        //For override
    }
    public void OnHit(float damage)
    {
        if (!IsDead)
        {
            hp -= damage;

            if (IsDead)
            {
                hp = 0;
                OnDeath();
            }

            healthBar.SetNewHP(hp);
            Instantiate(combatTextPrefab, transform.position + 2f * Vector3.up, Quaternion.Euler(new Vector3(60, 0, 0))).OnInit(damage);
        }
    }



    public virtual void ResetAttack()
    {
        if (IsDead) 
            ChangeAnim(Constant.ANIM_DIE);
        else
        {
            isAttack = false;
            ChangeAnim(Constant.ANIM_IDLE);
        }
    }



    public void GetHealthBar(float hpMax)
    {
        HealthBar healthBarEnemy = Instantiate(healthBarPrefab);
        healthBarEnemy.OnInit(hpMax, transform);
        this.healthBar = healthBarEnemy;

    }

    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            //if (this is Hero)
            //{
            //    Debug.Log(animName);
            //}
            anim.ResetTrigger(currentAnimName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }
}
