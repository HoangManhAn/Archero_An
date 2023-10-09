using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    //float timer;
    //float randomTime;
    //public void OnEnter(Enemy enemy)
    //{
    //    timer = 0;
    //    randomTime = Random.Range(3f, 6f);
    //}

    //public void OnExecute(Enemy enemy)
    //{
    //    timer += Time.deltaTime;

    //    if (enemy.Target != null)
    //    {

    //        if (enemy.IsTargetInRange())
    //        {
    //            //Doi huong Enemy toi huong cua Player, attack player
    //            enemy.ChangeDirect();

    //            enemy.ChangeState(new AttackState());
                
    //        }
    //        else
    //        {
    //            //Doi huong Enemy toi huong cua Player, moving to player
    //            enemy.ChangeDirect();
    //            enemy.MoveAround();
    //        }
    //    }
    //    else
    //    {
    //        if (timer < randomTime)
    //        {
    //            enemy.MoveAround();
    //        }
    //        else
    //        {
    //            enemy.ChangeState(new IdleState());
    //        }
    //    }
    //}

    //public void OnExit(Enemy enemy) { }
}
