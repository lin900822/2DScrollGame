using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using PlayerControl;
using CoreComponents;

namespace PlayerControl
{
    public class PlayerController : MonoBehaviour
    {
        [Inject]
        private readonly IUserInput userInput = null;

        public Rigidbody2D PlayerRigidbody = null;
        public CapsuleCollider2D PlayerCollider = null;

        public Animator animator = null;

        public CinemechineShake PlayerCameraShake = null;

        [SerializeField] HealthPoint healthPoint = null;
        [SerializeField] PlayerMovement playerMovement = null;
        [SerializeField] PlayerAttack playerAttack = null;

        Vector2 playerInputAxis = Vector2.zero;

        bool isHurting = false;

        private void Start()
        {
            healthPoint.OnDamaged += hurt;
        }

        private void OnDestroy()
        {
            healthPoint.OnDamaged -= hurt;
        }

        private void Update()
        {
            getInput();
        }

        private void FixedUpdate()
        {
            move();
        }

        void getInput()
        {
            if (isHurting) return;

            playerInputAxis = new Vector2(userInput.GetAxis("Horizontal"), 0f);

            if (userInput.PressJump())
            {
                jump();
            }

            if (userInput.PressAttack())
            {
                attack();
            }

            if (userInput.PressDash())
            {
                dash();
            }
            else
            {
                endDash();
            }
        }

        void move()
        {
            if (isHurting) return;

            playerMovement.Move(playerInputAxis);

            if (playerMovement.IsGround())
            {
                animator.SetBool("isJump", false);

                if (PlayerRigidbody.velocity.x >= .1f || PlayerRigidbody.velocity.x <= -.1f)
                {
                    animator.SetBool("isWalk", true);
                }
                else
                    animator.SetBool("isWalk", false);
            }
            else
            {
                animator.SetBool("isJump", true);
                animator.SetBool("isWalk", false);
            }
        }

        void dash()
        {
            if ((PlayerRigidbody.velocity.x >= .1f || PlayerRigidbody.velocity.x <= -.1f) && playerMovement.IsGround())
            {
                playerMovement.Dash();
                animator.SetBool("isDash", true);
            }
        }

        void endDash()
        {
            playerMovement.EndDash();
            animator.SetBool("isDash", false);
        }

        void jump()
        {
            playerMovement.Jump();
            animator.SetBool("isJump", true);
        }

        void attack()
        {
            playerAttack.Attack();
        }

        void hurt(Transform atkTrans)
        {
            if (isHurting) return;

            PlayerCameraShake.Shake(CinemechineShakeType.Normal);

            animator.SetTrigger("Hurt");

            Vector2 atkDirection = atkTrans.position - transform.position;

            PlayerRigidbody.AddForce(new Vector2(-atkDirection.normalized.x, .3f) * 8f, ForceMode2D.Impulse);

            isHurting = true;

            Invoke(nameof(resetHurt), .5f);
        }

        void resetHurt()
        {
            isHurting = false;
            PlayerRigidbody.velocity = Vector2.zero;
        }
    }
}

