using Scripts.Events.Cutscenes;
using Scripts.Events.Player;
using UnityEngine;

namespace Scripts.Player
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class PlayerController : MonoBehaviour
    {
        private Health playerHealth;
        private Movement playerMovement;

        private void Awake()
        {
            playerHealth = GetComponent<Health>();
            playerMovement = GetComponent<Movement>();

            var playerRb = GetComponent<Rigidbody2D>();
            playerMovement.Initialize(playerRb);

            PlayerEvents.OnPlayerDamaged += playerHealth.Damage;
            CutsceneEvents.OnInitialFinished += playerMovement.OnFinishedInitCutscenes;
            PlayerEvents.OnPlayerKilled += playerMovement.OnPlayerKilled;
            PlayerEvents.OnPlayerKilled += () =>
            {
                PlayerEvents.OnPlayerDamaged -= playerHealth.Damage;
                CutsceneEvents.OnInitialFinished -= playerMovement.OnFinishedInitCutscenes;
                PlayerEvents.OnPlayerKilled -= playerMovement.OnPlayerKilled;
            };
        }
    }
}