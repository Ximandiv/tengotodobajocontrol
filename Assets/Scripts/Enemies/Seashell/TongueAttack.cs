using Scripts.Events.Player;
using UnityEngine;

namespace Scripts.Enemies.Seashell
{
    public class TongueAttack : MonoBehaviour
    {
        [SerializeField] private VisionRange visionRange;
        [SerializeField] private float orbitRadius = 2f;
        [SerializeField] private float trackingSpeed = 5f;
        [SerializeField] private float angleThreshold = 0.1f;

        private Transform shellLogic;
        private float currentAngle;
        private Vector2 lastPlayerPosition;
        private bool canAttack = false;

        private void Start()
        {
            shellLogic = transform.parent.parent.Find("Logic");

            if (shellLogic == null)
            {
                Debug.LogError("Shell Logic reference not found!");
                enabled = false;
                return;
            }

            currentAngle = Random.Range(0f, 360f);

            visionRange.OnPlayerInRange += setCanAttack;
        }

        private void OnDestroy()
        {
            visionRange.OnPlayerInRange -= setCanAttack;
        }

        private void setCanAttack(bool status)
            => canAttack = status;

        private void Update()
        {
            if (!canAttack) return;

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                lastPlayerPosition = player.transform.position;

                // Calculate the angle to the player
                Vector2 toPlayer = lastPlayerPosition - (Vector2)shellLogic.position;
                float targetAngle = Mathf.Atan2(toPlayer.y, toPlayer.x) * Mathf.Rad2Deg;

                // Normalize angles to 0-360 range
                if (targetAngle < 0) targetAngle += 360f;
                if (currentAngle < 0) currentAngle += 360f;

                // Find shortest path to target angle
                float angleDiff = Mathf.DeltaAngle(currentAngle, targetAngle);

                // Only move if we're not close enough to the target angle
                if (Mathf.Abs(angleDiff) > angleThreshold)
                {
                    float moveStep = Mathf.Sign(angleDiff) * trackingSpeed * Time.deltaTime * 360f;
                    // Don't overshoot the target
                    if (Mathf.Abs(moveStep) > Mathf.Abs(angleDiff))
                    {
                        currentAngle = targetAngle;
                    }
                    else
                    {
                        currentAngle += moveStep;
                    }
                }
            }

            // Keep angle in 0-360 range
            currentAngle = currentAngle % 360f;

            // Convert angle to radians
            float angleRad = currentAngle * Mathf.Deg2Rad;

            // Calculate position on the orbit
            Vector2 offset = new Vector2(
                Mathf.Cos(angleRad) * orbitRadius,
                Mathf.Sin(angleRad) * orbitRadius
            );

            // Update position
            transform.parent.position = (Vector2)shellLogic.position + offset;

            // Make the tongue face towards its next position on the orbit
            transform.parent.rotation = Quaternion.Euler(0, 0, currentAngle);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!canAttack) return;

            if (collision.CompareTag("Player"))
                PlayerEvents.InvokePlayerDamaged(-1);
        }
    }
}
