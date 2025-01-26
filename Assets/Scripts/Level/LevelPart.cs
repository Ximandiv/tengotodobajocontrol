using Scripts.Events.Level;
using UnityEngine;

namespace Scripts.Level
{
    public class LevelPart : MonoBehaviour
    {
        public string level = "One";

        [SerializeField]
        private GameStatus gameStatus;

        private void Awake()
        {
            LevelOneEvents.OnCheckpointLoaded += closeIfAlreadyPassed;
        }

        private void OnDestroy()
        {
            level = "N/A";
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
            if (collision.CompareTag("Player") && !gameStatus.IsPlayerDead)
            {
                GetComponent<BoxCollider2D>().isTrigger = false;
                transform.gameObject.tag = "Borders";
                invokeLevelPartFinished();
            }
        }

        private void invokeLevelPartFinished()
        {
            LevelOneEvents.InvokePartFinished(level);
        }
    }
}