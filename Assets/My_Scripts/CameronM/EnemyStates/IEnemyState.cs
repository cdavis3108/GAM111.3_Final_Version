using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    void Execute();
    void Enter(AIMedium enemy);
    void Exit();
    void OnTriggerEnter(Collider other);
}
