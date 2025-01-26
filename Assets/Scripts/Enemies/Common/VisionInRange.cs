using System;
using UnityEngine;

namespace Scripts.Enemies.Common
{
    public class VisionInRange : MonoBehaviour
    {
        public event Action OnPlayerInReach;
        public event Action OnPlayerOutOfReach;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.transform.CompareTag("Player"))
                OnPlayerInReach?.Invoke();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.transform.CompareTag("Player"))
                OnPlayerOutOfReach?.Invoke();
        }
    }
}