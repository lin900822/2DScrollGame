using UnityEditor;
using UnityEngine;

public class EnemyStateRun : IEnemyState
{
    readonly EnemyModel _enemyModel = null;
    readonly EnemyStateManager _stateManager = null;

    int faceDirection = 1;

    public EnemyStateRun(EnemyModel enemyModel, EnemyStateManager stateManager)
    {
        _enemyModel = enemyModel;
        _stateManager = stateManager;
    }

    public void EnterState()
    {
        _enemyModel.Animator.SetBool("isRun", true);
    }

    public void ExitState()
    {
        _enemyModel.Animator.SetBool("isRun", false);
    }

    public void FixedTick()
    {
        faceDirection = (_enemyModel.Transform.position.x - _enemyModel.TargetTransform.position.x) >= 0 ? -1 : 1;
        _enemyModel.LocalScale = new Vector2(-faceDirection, 1f);

        Vector2 moveVector = new Vector2(faceDirection, 0) * _enemyModel.SettingsConfiguration.RunSpeed;

        _enemyModel.Velocity = new Vector2(moveVector.x, _enemyModel.Velocity.y);
    }

    public void Tick()
    {
        if(_enemyModel.TargetTransform == null)
        {
            _stateManager.ChangeState(EnemyStateManager.States.Idle);
            return;
        }
    }
}