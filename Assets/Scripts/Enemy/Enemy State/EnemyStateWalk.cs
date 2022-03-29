using UnityEditor;
using UnityEngine;

public class EnemyStateWalk : IEnemyState
{
    readonly EnemyModel _enemyModel = null;
    readonly EnemyStateManager _stateManager = null;

    float timeToIdle = 0f;
    float timer = 0f;

    int faceDirection = 1;

    public EnemyStateWalk(EnemyModel enemyModel, EnemyStateManager stateManager)
    {
        _enemyModel = enemyModel;
        _stateManager = stateManager;
    }

    public void EnterState()
    {
        _enemyModel.Animator.SetBool("isWalk", true);

        timer = Time.time;
        timeToIdle = Random.Range(1.5f, 3f);

        faceDirection = Random.Range(0, 2) == 0 ? -1 : 1;

        _enemyModel.LocalScale = new Vector2(-faceDirection, 1f);
    }

    public void ExitState()
    {
        _enemyModel.Animator.SetBool("isWalk", false);
    }

    public void FixedTick()
    {
        if ((Time.time - timer) > timeToIdle)
        {
            _stateManager.ChangeState(EnemyStateManager.States.Idle);
            return;
        }

        move();
    }

    public void Tick()
    {
        
    }

    void move()
    {
        Vector2 moveVector = new Vector2(faceDirection, 0) * _enemyModel.SettingsConfiguration.WalkSpeed;

        _enemyModel.Velocity = new Vector2(moveVector.x, _enemyModel.Velocity.y);
    }
}