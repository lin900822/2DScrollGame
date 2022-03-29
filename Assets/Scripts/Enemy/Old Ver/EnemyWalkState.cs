using System.Collections;
using UnityEngine;

public class EnemyWalkState : EnemyState
{
    float timeToIdle = 0f;
    float timer = 0f;

    int faceDirection = 1;

    public EnemyWalkState(EnemyController enemyController) : base(enemyController)
    {
        enemyController.animator.SetBool("isWalk", true);

        timer = Time.time;
        timeToIdle = Random.Range(1.5f, 3f);

        faceDirection = Random.Range(0, 2) == 0 ? -1 : 1;

        enemyController.transform.localScale = new Vector2(-faceDirection, 1f);
    }

    public override void ExitState()
    {
        enemyController.animator.SetBool("isWalk", false);
    }

    public override void Tick()
    {
        if ((Time.time - timer) > timeToIdle)
        {
            enemyController.SetState(new EnemyIdleState(this.enemyController));
        }

        move();
    }

    void move()
    {
        Vector2 moveVector = enemyController.transform.TransformDirection(new Vector2(faceDirection, 0)) * enemyController.walkSpeed;

        enemyController.enemyRigbody.velocity = new Vector2(moveVector.x, enemyController.enemyRigbody.velocity.y);
    }
}