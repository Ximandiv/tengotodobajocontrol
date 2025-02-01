using Scripts.Events.Level;
using Scripts.Events.Player;
using UnityEngine;

namespace Scripts.Enemies.Fish_Sword
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Controller : MonoBehaviour
    {
        private Movement enemyMovement;
        private Rigidbody2D rb;

        [SerializeField]
        private string levelPart = "One";

        private int damageAmount = 1;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            enemyMovement = GetComponent<Movement>();
            enemyMovement.Initialize(rb);

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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("Player"))
            {
                PlayerEvents.InvokePlayerDamaged(damageAmount);
            }
        }
    }
}
