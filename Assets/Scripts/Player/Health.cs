using Scripts.Events.Player;
using UnityEngine;

namespace Scripts.Player
{
    public class Health : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField]
        private int hitPoints = 1;
        [SerializeField]
        private int minHitPoints = 0;

        public void Damage(int amount)
        {
            if (amount >= 0)
                return;

            var totalReduced = hitPoints + amount;
            if (totalReduced < minHitPoints)
                return;

            hitPoints += amount;

            if (hitPoints == minHitPoints)
                PlayerEvents.InvokePlayerKilled();
        }
    }
}