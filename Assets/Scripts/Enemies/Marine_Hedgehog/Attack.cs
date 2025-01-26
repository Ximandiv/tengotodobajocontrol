using Scripts.Events.Player;
using UnityEngine;

namespace Scripts.Enemies.Marine_Hedgehog
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class Attack : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField]
        private int damageAmount = -1;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Player"))
                PlayerEvents.InvokePlayerDamaged(damageAmount);
        }
    }
}
