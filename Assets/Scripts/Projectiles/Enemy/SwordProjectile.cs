using Scripts.Events.Player;
using System.Collections;
using UnityEngine;

namespace Scripts.Projectiles.Enemy
{
    public class SwordProjectile : MonoBehaviour
    {
        private Rigidbody2D rb;

        [SerializeField]
        private int speed = 3;
        [SerializeField]
        private int damage = -1;
        [SerializeField]
        private Vector3 directionToLastPlayerPos = Vector3.zero;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();

            directionToLastPlayerPos = (PlayerTracker.Instance.PlayerPosition - transform.position).normalized;

            rotateTowardPlayer();
        }

        private void OnDestroy()
        {
            directionToLastPlayerPos = Vector3.zero;
        }

        private void FixedUpdate()
        {
            move();
        }
        private void move()
        {
            rb.MovePosition(transform.position + directionToLastPlayerPos * speed * Time.fixedDeltaTime);
        }

        private void rotateTowardPlayer()
        {
            float angle = Mathf.Atan2(directionToLastPlayerPos.y, directionToLastPlayerPos.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.CompareTag("Player"))
            {
                PlayerEvents.InvokePlayerDamaged(damage);
                Destroy(gameObject);
            }
            else if (collision.transform.CompareTag("Borders"))
                Destroy(gameObject);
        }
    }
}