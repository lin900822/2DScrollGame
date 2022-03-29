using System.Collections;
using UnityEngine;

public class EnemyStateIdle : IEnemyState
{
    readonly EnemyStateManager _stateManager = null;

    float timeToWalk = 0f;
    float timer = 0f;

    public EnemyStateIdle(EnemyStateManager stateManager)
    {
        _stateManager = stateManager;
    }

    public void EnterState()
    {
        timer = Time.time;
        timeToWalk = Random.Range(1.5f, 3f);
    }

    public void ExitState()
    {
        
    }

    public void Tick()
    {
        if ((Time.time - timer) > timeToWalk)
        {
            _stateManager.ChangeState(EnemyStateManager.States.Walk);
            return;
        }
    }

    public void FixedTick()
    {
        
    }
}