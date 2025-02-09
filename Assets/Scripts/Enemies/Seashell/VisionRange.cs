using System;
using UnityEngine;

namespace Scripts.Enemies.Seashell
{
    public class VisionRange : MonoBehaviour
    {
        public event Action<bool> OnPlayerInRange;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;

            OnPlayerInRange?.Invoke(true);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;

            OnPlayerInRange?.Invoke(false);
        }
    }
}