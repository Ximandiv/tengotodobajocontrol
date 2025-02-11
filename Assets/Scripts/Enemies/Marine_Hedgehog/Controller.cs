using Scripts.Events.Level;
using Scripts.Events.Player;
using System.Collections;
using UnityEngine;

namespace Scripts.Enemies.Marine_Hedgehog
{
    public class Controller : MonoBehaviour
    {
        [SerializeField]
        private string levelPart = "One";
        [SerializeField]
        private int meleeDamageAmount = 1;
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private bool isIndestructible = false;

        private bool isDead = false;

        private void Awake()
        {
            LevelOneEvents.OnPartFinished += autoDestroy;
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

        private IEnumerator deathSequence()
        {
            yield return new WaitForSeconds(1.65f);
            Destroy(transform.parent.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(isDead) return;

            if(collision.CompareTag("PlayerProjectile")
                && !isIndestructible)
            {
                isDead = true;

                animator.SetBool("isDead", true);
                StartCoroutine(deathSequence());
            }
            else if (collision.CompareTag("Player"))
            {
                animator.SetBool("isAttacking", true);
                PlayerEvents.InvokePlayerDamaged(meleeDamageAmount);
            }
        }
    }
}