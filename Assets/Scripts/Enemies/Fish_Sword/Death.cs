using Scripts.Events.Player;
using System.Collections;
using UnityEngine;

namespace Scripts.Enemies.Fish_Sword
{
    public class Death : MonoBehaviour
    {
        private Movement enemyMovement;
        private Attack enemyAttack;
        private Animator animator;

        private int damageAmount = -1;
        private bool isDead = false;

        private void Awake()
        {
            enemyAttack = transform.parent.GetComponent<Attack>();
            enemyMovement = transform.parent.GetComponent<Movement>();
            animator = transform.parent.Find("Sprite").GetComponent<Animator>();
        }

        private IEnumerator deathSequence()
        {
            yield return new WaitForSeconds(1.25f);
            Destroy(transform.parent.parent.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (isDead) return;

            if (collision.CompareTag("PlayerProjectile"))
            {
                isDead = true;
                enemyMovement.enabled = false;
                enemyAttack.enabled = false;

                animator.SetBool("isDead", true);
                StartCoroutine(deathSequence());
            }
            else if (collision.CompareTag("Player"))
            {
                animator.SetBool("isAttacking", true);
                PlayerEvents.InvokePlayerDamaged(damageAmount);
            }
        }
    }
}