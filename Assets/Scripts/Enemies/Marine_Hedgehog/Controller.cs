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

        private void Awake()
        {
            switch (levelPart)
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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.CompareTag("Player"))
                PlayerEvents.InvokePlayerDamaged(meleeDamageAmount);
        }
    }
}