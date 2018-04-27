using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{
    private AIMedium enemy;
    private float patrolTimer;
    private float patrolDuration = 10;

    public void Enter(AIMedium enemy)
    {
        this.enemy = enemy;
        enemy.MyAnimator.SetBool("powerDown", false);
        enemy.idling = false;
    }

    public void Execute()
    {
        Patrol();
        enemy.moving = true;
        
        if (enemy.TargetAcquired != null)
        {
            enemy.MyAnimator.SetBool("hasTarget", true);
            enemy.ChangeState(new AttackState());
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

    private void Patrol()
    {
        patrolTimer += Time.deltaTime;

        if(patrolTimer >= patrolDuration)
        {
            enemy.ChangeState(new IdleState());
            enemy.moving = false;
            patrolTimer = 0;
        }
    }
}
