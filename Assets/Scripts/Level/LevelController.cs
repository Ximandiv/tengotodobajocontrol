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

            if (SceneManager.GetActiveScene().name == "Tutorial")
                gameStatus.IsInTutorial = true;

            PlayerEvents.OnPlayerKilled += RestartLevelHandler;
            SceneManager.sceneLoaded += onSceneChange;
        }

        private void onSceneChange(Scene scene, LoadSceneMode _)
        {
            switch(scene.name)
            {
                case "Tutorial":
                    gameStatus.IsInTutorial = true;
                    gameStatus.IsInLevelOne = false;
                    break;
                case "Level_One":
                    gameStatus.IsInTutorial = false;
                    gameStatus.IsInLevelOne = true;
                    break;
            }
        }

        private void RestartLevelHandler()
        {
            StartCoroutine(RestartLevel());
        }

        private void OnApplicationQuit()
        {
            PlayerEvents.OnPlayerKilled -= RestartLevelHandler;
            SceneManager.sceneLoaded -= onSceneChange;

            gameStatus.IsPlayerDead = false;
            gameStatus.StartCutsceneEnd = false;
            gameStatus.IsInTutorial = false;
            gameStatus.IsInLevelOne = false;
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