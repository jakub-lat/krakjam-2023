using System;
using Audio;
using UnityEngine;
using UnityEngine.UIElements;
using Utils;
using Cinemachine;

namespace Player
{
    public class PlayerMovement : MonoSingleton<PlayerMovement>
    {
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float jumpForce = 10f;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private Transform playerBody;
        
        private Rigidbody2D rb;

        private CinemachineTransposer vCamTransposer;

        private float lerpT;
        private float lerpFrom;
        private float lerpTo;
        private bool isLerping;
        private bool isGrounded;
        private bool wasGrounded;
        private bool isJumping = false;

        private PlayerBehaviour pb;
        private Animator anim;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            vCamTransposer = VCamInstance.Current.GetCinemachineComponent<CinemachineTransposer>();
            pb = PlayerBehaviour.Current;
            anim = GetComponent<Animator>();
        }

        private void Update()
        {
            if (pb.dead) return;

            IsGrounded();
            Move();
            Jump();
        }

        private void Move()
        {
            var horizontal = Input.GetAxis("Horizontal");
            // rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
            rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

            if (horizontal != 0)
            {
                playerBody.localRotation = Quaternion.Euler(new Vector3(playerBody.localRotation.eulerAngles.x,
                    horizontal > 0 ? 0 : 180, playerBody.localRotation.eulerAngles.z));


                anim.SetBool("Walking",isGrounded);

                    if (!isLerping)
                {
                    lerpFrom = vCamTransposer.m_FollowOffset.x;
                    lerpTo = Mathf.Sign(horizontal) * 3.5f;
                    lerpT = 0;
                    isLerping = true;
                }
                
                var offset = vCamTransposer.m_FollowOffset;
                offset.x = Mathf.Lerp(lerpFrom, lerpTo, Mathf.SmoothStep(0, 1, lerpT));
                vCamTransposer.m_FollowOffset = offset;

                lerpT += Time.deltaTime * 1.2f;
                
                PlayerSounds.Current.StartWalking();
            }
            else
            {
                isLerping = false;
                PlayerSounds.Current.StopWalking();
                anim.SetBool("Walking", false);
            }
        }

        private void IsGrounded()
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundMask);

            if (!wasGrounded && isGrounded)
            {
                PlayerSounds.Current.Land();
            }
            
            wasGrounded = isGrounded;
        }

        private void Jump()
        {
            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.Space))
            {
                isJumping = false;
                return;
            }
            if (isJumping) return;

            isJumping = true;
            

            if (!isGrounded) return;

            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            
            PlayerSounds.Current.Jump();
        }
    }
}
