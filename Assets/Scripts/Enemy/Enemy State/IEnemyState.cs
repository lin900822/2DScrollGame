using UnityEditor;
using UnityEngine;

public interface IEnemyState
{
    void EnterState();
    void ExitState();
    void Tick();
    void FixedTick();
}