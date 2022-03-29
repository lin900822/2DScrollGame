using System.Collections;
using UnityEngine;
using Zenject;

public class EnemyTargetDetecter : ITickable
{
    readonly EnemyModel _enemyModel;
    readonly EnemyStateManager _enemyStateManager;

    public EnemyTargetDetecter(EnemyModel enemyModel, EnemyStateManager enemyStateManager)
    {
        _enemyModel = enemyModel;
        _enemyStateManager = enemyStateManager;
    }

    public void Tick()
    {
        if (_enemyStateManager.CurrentState == EnemyStateManager.States.Hurt) return;

        _enemyModel.TargetTransform = DetectTarget();

        if (_enemyModel.TargetTransform != null)
        {
            _enemyStateManager.ChangeState(EnemyStateManager.States.Run);
        }
    }

    Transform DetectTarget()
    {
        Collider2D targetCollider = Physics2D.OverlapCircle(_enemyModel.Transform.position, _enemyModel.SettingsConfiguration.DetectTargetRadius, _enemyModel.SettingsConfiguration.TargetLayer);

        if (targetCollider != null)
            return targetCollider.transform;
        else
            return null;
    }
}