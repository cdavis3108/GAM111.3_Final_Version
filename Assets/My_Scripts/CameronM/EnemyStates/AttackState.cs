using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IEnemyState
{
    private AIMedium enemy;

    public void Enter(AIMedium enemy)
    {
        this.enemy = enemy;    
    }

    public void Execute()
    {
        if (enemy.targetAcquired != null)
        {
            enemy.spotlight.color = Color.red;
            if (enemy.coward)
            {
                //Do nothing, can't attack
            }
            else
            {
                enemy.attackTimer += Time.time;
                if (enemy.attackTimer >= enemy.attackDelay)
                {
                    enemy.attackTimer = 0;
                    if (enemy.ranged)
                    {
                        //Ranged
                        enemy.RangedAttack();
                    }
                    else
                    {
                        //Melee
                        enemy.MeleeAttack();
                    }
                }

            }
        } else
        {
            enemy.ChangeState(new PatrolState());
        }
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        
    }
}
