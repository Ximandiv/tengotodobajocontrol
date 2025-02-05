using Scripts.Events.Level;
using Scripts.Events.Player;
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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.CompareTag("Player"))
            {
                animator.SetBool("isAttacking", true);
                PlayerEvents.InvokePlayerDamaged(meleeDamageAmount);
            }
        }
    }
}