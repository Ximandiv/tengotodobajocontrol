using Scripts.Events.Player;
using UnityEngine;

namespace Scripts.Enemies.Fish_Gun
{
    public class Hitbox : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
                PlayerEvents.InvokePlayerDamaged(-1);
        }
    }
}