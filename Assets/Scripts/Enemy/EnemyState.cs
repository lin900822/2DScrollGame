using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    protected EnemyController enemyController;

    public EnemyState(EnemyController enemyController)
    {
        this.enemyController = enemyController;
    }

    public abstract void Tick();

    public abstract void ExitState();
}
