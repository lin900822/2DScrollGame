using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerControl;
using CoreComponents;

namespace PlayerControl
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] PlayerController playerController = null;

        [SerializeField] Transform attackPoint = null;
        [SerializeField] LayerMask enemyLayer = default;
        [SerializeField] float attackRange = .5f; 

        float firstAtkTime = -999f;
        float secondAtkTime = -999f;

        public void Attack()
        {
            if (Time.time - secondAtkTime <= 1f)
            {
                specialAttack();
                firstAtkTime = -999f;
                secondAtkTime = -999f;
            }
            else
            {
                if (Time.time - firstAtkTime <= 1f)
                {
                    secondAtkTime = Time.time;
                    normalAttack();
                }
                else
                {
                    firstAtkTime = Time.time;
                    normalAttack();
                }
            }
        }

        void normalAttack()
        {
            playerController.animator.SetTrigger("Attack");
            bool hasGivenDamage = giveDamage(10f);

            if (hasGivenDamage)
                playerController.PlayerCameraShake.Shake(CinemechineShakeType.Normal);
        }

        void specialAttack()
        {
            playerController.animator.SetTrigger("Strike");
            bool hasGivenDamage = giveDamage(20f);

            if (hasGivenDamage)
                playerController.PlayerCameraShake.Shake(CinemechineShakeType.Hard);
        }

        bool giveDamage(float damage)
        {
            Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

            for (int i = 0; i < enemyColliders.Length; i++)
            {
                enemyColliders[i].GetComponent<HealthPoint>().TakeDamage(this.transform, damage);
            }

            if (enemyColliders.Length >= 1)
            {
                return true;
            }

            return false;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}

