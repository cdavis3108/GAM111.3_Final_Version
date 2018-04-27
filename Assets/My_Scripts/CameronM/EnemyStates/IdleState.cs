using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState
{
    private AIMedium enemy;
    private float Timer;
    private float idleDuration = 10f;
    //private float sleepDuraction = 10f;

    private bool patrolling;

    public void Enter(AIMedium enemy)
    {
        this.enemy = enemy;
        Timer = 0;
        patrolling = false;
    }

    public void Execute()
    {
        if (!patrolling)
        {
            Idle();
        } else
        {
            enemy.ChangeState(new PatrolState());
        }

        if (enemy.TargetAcquired != null)
        {
            enemy.MyAnimator.SetBool("hasTarget", true);
            enemy.ChangeState(new PatrolState());
        } else
        {
            enemy.MyAnimator.SetBool("hasTarget", false);
        }
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {

    }

    private void Idle()
    {
        enemy.idling = true;
        enemy.MyAnimator.SetBool("powerDown", false);
        Timer += Time.deltaTime;

        if(Timer >= idleDuration)
        {
            if (enemy.patrols)
            {
                enemy.ChangeState(new PatrolState());
            }
            else
            {
                patrolling = true;
            }
            Timer = 0;
        }
    }

    /*
    private void PowerDown()
    {
        enemy.MyAnimator.SetBool("powerDown", true);
        Timer += Time.deltaTime;

        if (Timer >= sleepDuraction)
        {
            powerDown = false;
            Timer = 0;
        }
    }
    */
}
