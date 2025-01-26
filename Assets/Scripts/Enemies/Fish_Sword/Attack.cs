using Scripts.Enemies.Common;
using Scripts.Projectiles.Enemy;
using System.Collections;
using UnityEngine;

namespace Scripts.Enemies.Fish_Sword
{
    public class Attack : MonoBehaviour
    {
        public VisionInRange hitboxToAttack;

        [SerializeField]
        private Transform projectile;
        [SerializeField]
        private int cooldownPerAttack = 5;

        private bool inRange = false;
        private Coroutine attackCoroutine;

        private void Awake()
        {
            hitboxToAttack.OnPlayerInReach += startAttack;
            hitboxToAttack.OnPlayerOutOfReach += stopAttack;
        }

        private void OnDestroy()
        {
            hitboxToAttack.OnPlayerInReach -= startAttack;
            hitboxToAttack.OnPlayerOutOfReach -= stopAttack;

            stopAttack();
        }

        private void startAttack()
        {
            inRange = true;

            if (attackCoroutine == null)
            {
                attackCoroutine = StartCoroutine(distanceAttack());
            }
        }

        private void stopAttack()
        {
            inRange = false;

            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
        }

        private IEnumerator distanceAttack()
        {
            while (inRange)
            {
                Instantiate(projectile.gameObject, transform.position, Quaternion.identity);

                yield return new WaitForSeconds(cooldownPerAttack);
            }

            attackCoroutine = null;
        }
    }
}