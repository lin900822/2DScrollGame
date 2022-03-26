using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerControl;

namespace PlayerControl
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] PlayerController playerController = null;

        [SerializeField] Transform footTrans = null;
        [SerializeField] LayerMask floorLayer = default;
        [SerializeField] float detectFloorRange = .1f;

        [SerializeField] float moveSpeed = 5f;
        [SerializeField] float jumpForce = 10f;

        public void Move(Vector2 playerInputAxis)
        {
            Vector2 moveVector = transform.TransformDirection(playerInputAxis) * moveSpeed;

            playerController.PlayerRigidbody.velocity = new Vector2(moveVector.x, playerController.PlayerRigidbody.velocity.y);

            if (moveVector.x < -0.1f)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (moveVector.x > 0.1f)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }

        }

        public void Dash()
        {
            playerController.PlayerCollider.offset = new Vector2(0.06f, -1.13f);
            playerController.PlayerCollider.size = new Vector2(2, 1);
            playerController.PlayerCollider.direction = CapsuleDirection2D.Horizontal;
        }

        public void EndDash()
        {
            playerController.PlayerCollider.offset = new Vector2(0.06f, -0.67f);
            playerController.PlayerCollider.size = new Vector2(1, 2);
            playerController.PlayerCollider.direction = CapsuleDirection2D.Vertical;
        }

        public void Jump()
        {
            if (IsGround())
                playerController.PlayerRigidbody.velocity = (Vector2.up * jumpForce);
        }

        public bool IsGround() => Physics2D.OverlapCircle(footTrans.position, detectFloorRange, floorLayer) != null;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(footTrans.position, detectFloorRange);
        }
    }
}

