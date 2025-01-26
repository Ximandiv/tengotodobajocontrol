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

            switch(levelPart)
            {
                case "One":
                    LevelOneEvents.OnPartOneFinished += autoDestroy;
                    break;
                case "Two":
                    LevelOneEvents.OnPartTwoFinished += autoDestroy;
                    break;
                case "Three":
                    LevelOneEvents.OnPartThreeFinished += autoDestroy;
                    break;
                case "Four":
                    LevelOneEvents.OnPartFourFinished += autoDestroy;
                    break;
                default:
                    break;
            }
        }

        private void OnDestroy()
        {
            switch (levelPart)
            {
                case "One":
                    LevelOneEvents.OnPartOneFinished -= autoDestroy;
                    break;
                case "Two":
                    LevelOneEvents.OnPartTwoFinished -= autoDestroy;
                    break;
                case "Three":
                    LevelOneEvents.OnPartThreeFinished -= autoDestroy;
                    break;
                case "Four":
                    LevelOneEvents.OnPartFourFinished -= autoDestroy;
                    break;
                default:
                    break;
            }
        }

        private void autoDestroy()
            => Destroy(gameObject);

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("Player"))
            {
                PlayerEvents.InvokePlayerDamaged(damageAmount);
            }
        }
    }
}
