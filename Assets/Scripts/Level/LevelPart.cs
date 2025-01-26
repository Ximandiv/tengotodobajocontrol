using Scripts.Events.Level;
using UnityEngine;

namespace Scripts.Level
{
    public class LevelPart : MonoBehaviour
    {
        public string level = "One";

        private void Awake()
        {
            LevelOneEvents.OnCheckpointLoaded += closeIfAlreadyPassed;
        }

        private void OnDestroy()
        {
            LevelOneEvents.OnCheckpointLoaded -= closeIfAlreadyPassed;
        }

        private void closeIfAlreadyPassed(string levelCheckpoint)
        {
            if (levelCheckpoint == level)
            {
                GetComponent<BoxCollider2D>().isTrigger = false;
                transform.gameObject.tag = "Borders";
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                GetComponent<BoxCollider2D>().isTrigger = false;
                transform.gameObject.tag = "Borders";
                invokeLevelPartFinished();
            }
        }

        private void invokeLevelPartFinished()
        {
            switch (level)
            {
                case "One":
                    LevelOneEvents.InvokePartOneFinished();
                    break;
                case "Two":
                    LevelOneEvents.InvokePartTwoFinished();
                    break;
                case "Three":
                    LevelOneEvents.InvokePartThreeFinished();
                    break;
                case "Four":
                    LevelOneEvents.InvokePartFourFinished();
                    break;
                default:
                    break;
            }
        }
    }
}