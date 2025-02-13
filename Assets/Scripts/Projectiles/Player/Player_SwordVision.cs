using System;
using UnityEngine;

namespace Scripts.Projectiles.Player
{
    public class Player_SwordVision : MonoBehaviour
    {
        public static event Action<Vector3> OnFirstEnemyInSight;
        private bool isLocked = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (isLocked) return;

            if(collision.CompareTag("Enemy"))
            {
                isLocked = true;
                OnFirstEnemyInSight?.Invoke(collision.transform.position);
            }
        }
    }
}