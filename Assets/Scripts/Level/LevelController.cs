using Scripts.Events.Player;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Level
{
    public class LevelController : MonoBehaviour
    {
        public static Transform Instance;

        [SerializeField]
        private GameStatus gameStatus;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = transform;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }
            
            gameStatus.IsInTutorial = true;
            PlayerEvents.OnPlayerKilled += RestartLevelHandler;
        }

        private void RestartLevelHandler()
        {
            StartCoroutine(RestartLevel());
        }

        private void OnDestroy()
        {
            PlayerEvents.OnPlayerKilled -= RestartLevelHandler;
            gameStatus.IsPlayerDead = false;
            gameStatus.StartCutsceneEnd = false;
            gameStatus.IsInTutorial = false;
        }

        private IEnumerator RestartLevel()
        {
            yield return new WaitForSeconds(3);
            var sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName);
            gameStatus.IsPlayerDead = false;
        }
    }
}