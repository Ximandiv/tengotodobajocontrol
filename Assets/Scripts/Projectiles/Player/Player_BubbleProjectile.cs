using System.Collections;
using UnityEngine;

namespace Scripts.Projectiles.Player
{
    public class Player_BubbleProjectile : MonoBehaviour
    {
        private Rigidbody2D rb;
        private SpriteRenderer spriteRenderer;
        private Vector2 moveDirection = Vector2.right;

        [SerializeField]
        private int speed = 3;

        public void InitializeDirection(bool isFlipped)
        {
            moveDirection = isFlipped ? Vector2.left : Vector2.right;

            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = isFlipped;
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            move();
        }
        private void move()
        {
            rb.linearVelocity = moveDirection * speed;
        }
    }
}