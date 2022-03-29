using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyStateManager : ITickable, IFixedTickable, IInitializable
{
    public enum States
    {
        None,
        Idle,
        Walk,
        Run,
        Hurt
    }

    IEnemyState _enemyStateEntity = null;

    States _state = States.None;

    private Dictionary<States, IEnemyState> _stateDictionary = new Dictionary<States, IEnemyState>();

    // 避免循環依賴，因此不能使用建構子注入
    [Inject]
    public void Construct(EnemyStateIdle idle, EnemyStateWalk walk, EnemyStateRun run, EnemyStateHurt hurt)
    {
        _stateDictionary.Add(States.Idle, idle);
        _stateDictionary.Add(States.Walk, walk);
        _stateDictionary.Add(States.Run, run);
        _stateDictionary.Add(States.Hurt, hurt);
    }

    public void Initialize()
    {
        ChangeState(States.Idle);
    }

    public void Tick()
    {
        if (_enemyStateEntity != null)
            _enemyStateEntity.Tick();
    }

    public void FixedTick()
    {
        if (_enemyStateEntity != null)
            _enemyStateEntity.FixedTick();
    }

    public States CurrentState => _state;

    public void ChangeState(States newState)
    {
        if (_state == newState) return;

        _state = newState;

        if (_enemyStateEntity != null)
        {
            _enemyStateEntity.ExitState();
            _enemyStateEntity = null;
        }

        _enemyStateEntity = _stateDictionary[newState];
        _enemyStateEntity.EnterState();
    }
}