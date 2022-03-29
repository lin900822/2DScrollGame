using System.Collections;
using UnityEngine;

public class EnemyStateHurt : IEnemyState
{
    readonly EnemyModel _enemyModel = null;
    readonly EnemyStateManager _stateManager = null;

    float timeToIdle = 0.5f;
    float timer = 0f;

    public EnemyStateHurt(EnemyModel enemyModel, EnemyStateManager stateManager)
    {
        _enemyModel = enemyModel;
        _stateManager = stateManager;
    }

    public void EnterState()
    {
        timer = Time.time;
        _enemyModel.Animator.SetTrigger("Hurt");
    }

    public void ExitState()
    {
        
    }

    public void FixedTick()
    {
        
    }

    public void Tick()
    {
        if ((Time.time - timer) > timeToIdle)
        {
            _stateManager.ChangeState(EnemyStateManager.States.Idle);
        }
    }
}