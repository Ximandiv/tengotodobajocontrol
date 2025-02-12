using Scripts.Events.Level;
using Scripts.Events.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static Transform Instance;

        public GameStatus gameStatus;

        public AudioClip actualTutorial;
        public List<AudioClip> tutorialPartOne = new();
        public List<AudioClip> tutorialPartTwo = new();
        public AudioClip tutorialDuel;
        public AudioClip tutorialClimax;
        public AudioClip tutorialBoss;

        private AudioSource audioSource;

        private string currentPart = "One";

        public void changeSongOnPartInTutorial(string newPart)
        {
            if (audioSource is null)
                return;

            currentPart = newPart;

            StopAllCoroutines();

            audioSource.Stop();

            if (currentPart == "One")
            {
                audioSource.clip = tutorialPartOne[1];
                audioSource.loop = true;
                audioSource.Play();
            }
            else if (currentPart == "Two")
            {
                audioSource.clip = tutorialPartTwo[0];
                audioSource.loop = false;
                audioSource.Play();
                StartCoroutine(WaitForAudioToEnd());
            }
            else if(currentPart == "Duel")
            {
                audioSource.clip = tutorialDuel;
                audioSource.loop = true;
                audioSource.Play();
            }
            else if (currentPart == "Three")
            {
                audioSource.clip = tutorialClimax;
                audioSource.loop = true;
                audioSource.Play();
            }
            else if (currentPart == "Four")
            {
                audioSource.clip = tutorialBoss;
                audioSource.loop = true;
                audioSource.Play();
            }
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = transform;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            audioSource = GetComponent<AudioSource>();

            audioSource.volume = 0.5f;
        }

        private void Start()
        {
            if (gameStatus.IsInTutorial)
            {
                audioSource.clip = actualTutorial;
                audioSource.loop = true;
                audioSource.Play();
            }

            LevelOneEvents.OnPartFinished += changeSongOnPartInTutorial;
            SceneManager.sceneLoaded += (Scene scene, LoadSceneMode loadMode) =>
            {
                StopAllCoroutines();
                audioSource.Stop();

                if(scene.name == "Tutorial")
                {
                    audioSource.clip = actualTutorial;
                    audioSource.Play();
                }
                else if (scene.name == "Level_One")
                {
                    audioSource.clip = tutorialPartOne[0];
                    audioSource.Play();
                }
            };
        }

        private void OnDestroy()
        {
            audioSource = null;
            StopAllCoroutines();
        }

        private IEnumerator WaitForAudioToEnd()
        {
            yield return new WaitForSeconds(audioSource.clip.length);
            OnAudioFinished();
        }

        private void OnAudioFinished()
        {
            if (currentPart == "One")
            {
                audioSource.clip = tutorialPartOne[1];
                audioSource.loop = true;
                audioSource.Play();
            }
            else if(currentPart == "Two")
            {
                audioSource.clip = tutorialPartTwo[1];
                audioSource.loop = true;
                audioSource.Play();
            }
        }
    }
}
