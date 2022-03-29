using System.Collections;
using UnityEngine;

public class EnemyRunState : EnemyState
{
    int faceDirection = 1;

    public EnemyRunState(EnemyController enemyController) : base(enemyController)
    {
        enemyController.animator.SetBool("isRun", true);
    }

    public override void ExitState()
    {
        enemyController.animator.SetBool("isRun", false);
    }

    public override void Tick()
    {
        if(enemyController.Target == null)
        {
            enemyController.SetState(new EnemyIdleState(enemyController));
            return;
        }

        faceDirection = (enemyController.transform.position.x - enemyController.Target.transform.position.x) >= 0 ? -1 : 1;
        enemyController.transform.localScale = new Vector2(-faceDirection, 1f);

        Vector2 moveVector = enemyController.transform.TransformDirection(new Vector2(faceDirection, 0)) * enemyController.runSpeed;

        enemyController.enemyRigbody.velocity = new Vector2(moveVector.x, enemyController.enemyRigbody.velocity.y);
    }
}