using System;
using UnityEngine;

namespace Scripts.Events.Player
{
    public static class PlayerEvents
    {
        public static event Action OnPlayerKilled;
        public static event Action<Vector2> OnPlayerMoving;
        public static event Action<int> OnPlayerDamaged;

        public static void InvokePlayerKilled()
            => OnPlayerKilled?.Invoke();

        public static void InvokePlayerDamaged(int damageAmount)
            => OnPlayerDamaged?.Invoke(damageAmount);

        public static void InvokePlayerMoving(Vector2 playerDirection)
            => OnPlayerMoving?.Invoke(playerDirection);
    }
}