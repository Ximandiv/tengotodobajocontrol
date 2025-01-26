using System.Collections;
using UnityEngine;

namespace Scripts.Projectiles.Player
{
    public class BubbleProjectile : MonoBehaviour
    {
        private Rigidbody2D rb;
        private SpriteRenderer spriteRenderer;
        private Animator animator;
        private Vector2 moveDirection = Vector2.right;
        private bool isDying = false;

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
            animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            move();
        }
        private void move()
        {
            rb.linearVelocity = moveDirection * speed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (isDying)
                return;

            if (collision.transform.CompareTag("Enemy"))
                if(!isDying)
                    StartCoroutine(destroyBubble(collision));
            else if(collision.transform.CompareTag("EnemyProjectile"))
                if (!isDying)
                    StartCoroutine(destroyBubble(collision, true));
            else if (collision.transform.CompareTag("Borders"))
                if (!isDying)
                    StartCoroutine(destroyBubble(collision));
        }

        private IEnumerator destroyBubble(Collider2D collision, bool isProjectile = false)
        {
            if(!isProjectile)
                Destroy(collision.transform.parent.gameObject);
            else
                Destroy(collision.gameObject);

            isDying = true;

            animator.SetBool("touched", true);
            yield return new WaitForSeconds(0.31f);
        }
    }
}