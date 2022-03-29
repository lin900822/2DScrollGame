using System.Collections;
using UnityEngine;

public class EnemyHurtState : EnemyState
{
    float timeToIdle = 0.5f;
    float timer = 0f;

    public EnemyHurtState(EnemyController enemyController) : base(enemyController)
    {
        timer = Time.time;
        enemyController.animator.SetTrigger("Hurt");
    }

    public override void ExitState()
    {

    }

    public override void Tick()
    {
        if ((Time.time - timer) > timeToIdle)
        {
            enemyController.SetState(new EnemyWalkState(this.enemyController));
        }
    }
}