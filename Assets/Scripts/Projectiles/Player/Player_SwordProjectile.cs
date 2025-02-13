using UnityEngine;

namespace Scripts.Projectiles.Player
{
    public class Player_SwordProjectile : MonoBehaviour
    {
        private Rigidbody2D rb;

        [SerializeField]
        private int speed = 6;
        [SerializeField]
        private Vector2 moveDirection = Vector2.zero;
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        private int awakeRotOffset = 90;
        private int onEnemyRotOffset = -90;

        public void InitializeDirection(bool isFlipped)
        {
            moveDirection = isFlipped ? Vector2.left : Vector2.right;

            if (!isFlipped)
                awakeRotOffset = -90;
            else
                awakeRotOffset = 90;

            transform.rotation = Quaternion.Euler(0, 0, awakeRotOffset);

            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = isFlipped;
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            Player_SwordVision.OnFirstEnemyInSight += onEnemySight;
        }

        private void FixedUpdate()
        {
            move();
        }

        private void OnDestroy()
        {
            Player_SwordVision.OnFirstEnemyInSight -= onEnemySight;
        }

        private void move()
        {
            rb.linearVelocity = moveDirection * speed;
        }

        private void onEnemySight(Vector3 position)
        {
            moveDirection = (position - transform.position).normalized;
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle + onEnemyRotOffset);
        }
    }
}