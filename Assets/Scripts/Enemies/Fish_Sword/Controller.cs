using Scripts.Events.Level;
using Scripts.Events.Player;
using System;
using System.Collections;
using UnityEngine;

namespace Scripts.Enemies.Fish_Sword
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Controller : MonoBehaviour
    {
        public event Action<bool> OnFlipRight;

        private Movement enemyMovement;
        private Attack enemyAttack;
        private Rigidbody2D rb;
        private Transform spriteTransform;

        [SerializeField]
        private string levelPart = "One";

        private bool isFacingRight = false;
        private Animator animator;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            enemyMovement = GetComponent<Movement>();
            enemyAttack = GetComponent<Attack>();
            spriteTransform = transform.Find("Sprite");
            animator = spriteTransform.GetComponent<Animator>();

            enemyAttack.Initialize(animator, this);
            enemyMovement.Initialize(rb);

            LevelOneEvents.OnPartFinished += autoDestroy;
        }

        private void Update()
        {
            if (!enemyAttack.inRange) return;
            
            float direction = PlayerTracker.Instance.PlayerPosition.x - transform.position.x;

            if (direction < 0 && isFacingRight)
            {
                OnFlipRight?.Invoke(false);
                flip();
            }
            else if (direction > 0 && !isFacingRight)
            {
                OnFlipRight?.Invoke(true);
                flip();
            }
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
    }
}
