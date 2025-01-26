using Scripts.Events.Player;
using UnityEngine;

namespace Scripts.Player
{
    public class PlayerAudio : MonoBehaviour
    {
        private AudioSource audioSource;

        [SerializeField]
        private AudioClip death;
        [SerializeField]
        private AudioClip shootingBubble;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.volume = 0.5f;

            PlayerEvents.OnPlayerKilled += DeathSound;
            PlayerEvents.OnPlayerShootingBubbles += ShootingBubble;
        }

        private void OnDestroy()
        {
            PlayerEvents.OnPlayerKilled -= DeathSound;
            PlayerEvents.OnPlayerShootingBubbles -= ShootingBubble;
        }

        private void DeathSound()
        {
            audioSource.Stop();
            audioSource.clip = death;
            audioSource.loop = false;
            audioSource.Play();
        }

        private void ShootingBubble(bool isShooting)
        {
            if (!isShooting) return;

            audioSource.Stop();
            audioSource.pitch = 2f;
            audioSource.clip = shootingBubble;
            audioSource.loop = false;
            audioSource.Play();
        }
    }
}
