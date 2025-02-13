using Scripts.Events.Cutscenes;
using Scripts.Events.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Player
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class PlayerController : MonoBehaviour
    {
        private Health playerHealth;
        private Attack playerAttack;
        private Movement playerMovement;

        [SerializeField]
        private GameStatus gameStatus;

        private void Awake()
        {
            playerHealth = GetComponent<Health>();
            playerMovement = GetComponent<Movement>();
            playerAttack = GetComponent<Attack>();

            var playerRb = GetComponent<Rigidbody2D>();
            playerMovement.Initialize(playerRb);

            PlayerEvents.OnPlayerDamaged += playerHealth.Damage;
            CutsceneEvents.OnInitialFinished += activateMovement;
            CutsceneEvents.OnInitialFinished += activateAttack;
            PlayerEvents.OnPlayerKilled += playerMovement.OnPlayerKilled;
            PlayerEvents.OnPlayerKilled += playerKilledSequence;
            PlayerEvents.OnPlayerKilled += disableAttack;
        }

        private void Start()
        {
            if (gameStatus.IsInTutorial
                && !gameStatus.StartCutsceneEnd)
            {
                playerAttack.enabled = false;
                playerMovement.enabled = false;
            }
        }

        private void OnDestroy()
        {
            PlayerEvents.OnPlayerDamaged -= playerHealth.Damage;
            CutsceneEvents.OnInitialFinished -= activateMovement;
            CutsceneEvents.OnInitialFinished -= activateAttack;
            PlayerEvents.OnPlayerKilled -= playerMovement.OnPlayerKilled;
            PlayerEvents.OnPlayerKilled -= playerKilledSequence;
            PlayerEvents.OnPlayerKilled -= disableAttack;
        }

        private void playerKilledSequence()
        {
            PlayerEvents.OnPlayerDamaged -= playerHealth.Damage;
            CutsceneEvents.OnInitialFinished -= activateMovement;
            CutsceneEvents.OnInitialFinished -= activateAttack;
            PlayerEvents.OnPlayerKilled -= playerMovement.OnPlayerKilled;
            PlayerEvents.OnPlayerKilled -= playerKilledSequence;
            PlayerEvents.OnPlayerKilled -= disableAttack;
            gameStatus.IsPlayerDead = true;
        }

        private void activateMovement()
            => playerMovement.enabled = true;

        private void activateAttack()
            => playerAttack.enabled = true;

        private void disableAttack()
            => playerAttack.enabled = false;
    }
}