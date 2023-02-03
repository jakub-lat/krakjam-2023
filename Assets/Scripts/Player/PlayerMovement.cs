using System;
using UnityEngine;
using UnityEngine.UIElements;
using Utils;

namespace Player
{
    public class PlayerMovement : MonoSingleton<PlayerMovement>
    {
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float jumpForce = 10f;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private Transform groundCheck;

        private Rigidbody2D rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Move();
            Jump();
        }

        private void Move()
        {
            var horizontal = Input.GetAxis("Horizontal");
            // rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
            rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        }

        private void Jump()
        {
            if (!Input.GetKeyDown(KeyCode.W)) return;
            
            var isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundMask);
            if (!isGrounded) return;
            
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

        }
    }
}
