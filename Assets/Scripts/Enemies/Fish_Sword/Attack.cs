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
        private int cooldownPerAttack = 3;

        [SerializeField]
        private float animationTime = 0.8f;
        private float animationTimeOffset = 0.1f;

        private bool inRange = false;
        private Animator animator;
        private Coroutine attackCoroutine;

        public void Initialize(Animator spriteAnimator)
            => animator = spriteAnimator;

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
            animator.SetBool("isAttacking", false);

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
                animator.SetBool("isAttacking", true);

                yield return new WaitForSeconds(animationTime - animationTimeOffset);

                Instantiate(projectile.gameObject, transform.position, Quaternion.identity);

                yield return new WaitForSeconds(animationTimeOffset);

                animator.SetBool("isAttacking", false);

                yield return new WaitForSeconds(cooldownPerAttack);
            }

            attackCoroutine = null;
        }
    }
}