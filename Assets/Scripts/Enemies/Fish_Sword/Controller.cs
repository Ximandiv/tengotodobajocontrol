using Scripts.Events.Level;
using Scripts.Events.Player;
using UnityEngine;

namespace Scripts.Enemies.Fish_Sword
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Controller : MonoBehaviour
    {
        private Movement enemyMovement;
        private Attack enemyAttack;
        private Rigidbody2D rb;
        private Transform spriteTransform;

        [SerializeField]
        private string levelPart = "One";

        private int damageAmount = -1;
        private bool isFacingRight = false;
        private Animator animator;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            enemyMovement = GetComponent<Movement>();
            enemyAttack = GetComponent<Attack>();
            spriteTransform = transform.Find("Sprite");
            animator = spriteTransform.GetComponent<Animator>();

            enemyAttack.Initialize(animator);
            enemyMovement.Initialize(rb);

            LevelOneEvents.OnPartFinished += autoDestroy;
        }

        private void Update()
        {
            if (!enemyAttack.inRange) return;
            
            float direction = PlayerTracker.Instance.PlayerPosition.x - transform.position.x;

            if (direction < 0 && isFacingRight)
                flip();
            else if (direction > 0 && !isFacingRight)
                flip();
        }

        private void flip()
        {
            isFacingRight = !isFacingRight;

            spriteTransform.GetComponent<SpriteRenderer>().flipX = isFacingRight;
        }

        private void OnDestroy()
        {
            LevelOneEvents.OnPartFinished -= autoDestroy;
        }

        private void autoDestroy(string level)
        {
            if(level == levelPart)
                transform.parent.gameObject.SetActive(false);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("Player"))
            {
                animator.SetBool("isAttacking", true);
                PlayerEvents.InvokePlayerDamaged(damageAmount);
            }
        }
    }
}
