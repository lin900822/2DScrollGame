using System.Collections;
using UnityEngine;


public class EnemyIdleState : EnemyState
{
    float timeToWalk = 0f;
    float timer = 0f;

    public EnemyIdleState(EnemyController enemyController) : base(enemyController) 
    {
        timer = Time.time;
        timeToWalk = Random.Range(1.5f, 3f);
    }

    public override void ExitState()
    {
        
    }

    public override void Tick()
    {
        if((Time.time - timer) > timeToWalk)
        {
            enemyController.SetState(new EnemyWalkState(this.enemyController));
        }
    }
}
