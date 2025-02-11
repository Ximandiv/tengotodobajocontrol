using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Events.Level
{
    public class GoalEvent : MonoBehaviour
    {
        [SerializeField]
        private string nextSceneName;
        [SerializeField]
        private GameStatus gameStatus;

        private string sceneName;

        private void Awake()
        {
            sceneName = SceneManager.GetActiveScene().name;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            switch(sceneName)
            {
                case "Tutorial":
                    gameStatus.IsInTutorial = false;
                    gameStatus.IsInLevelOne = true;
                    SceneManager.LoadSceneAsync(nextSceneName);
                    break;
                case "Level_One":
                    gameStatus.IsInLevelOne = false;
                    break;
            }
        }
    }
}
