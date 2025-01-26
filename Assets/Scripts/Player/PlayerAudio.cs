using Scripts.Events.Player;
using UnityEngine;

namespace Scripts.Player
{
    public class PlayerAudio : MonoBehaviour
    {
        private AudioSource audioSource;

        [SerializeField]
        private AudioClip deathClip;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.volume = 0.5f;

            PlayerEvents.OnPlayerKilled += DeathSound;
        }

        private void OnDestroy()
        {
            PlayerEvents.OnPlayerKilled -= DeathSound;
        }

        private void DeathSound()
        {
            audioSource.clip = deathClip;
            audioSource.loop = false;
            audioSource.Play();
        }
    }
}
