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
        private Transform projectileSpawn;
        private Vector3 originalProjSpawnPos;
        [SerializeField]
        private float cooldownPerAttack = 3;

        [SerializeField]
        private float animationTime = 0.8f;
        private float animationTimeOffset = 0.1f;

        public bool inRange = false;
        private Controller enemyController;
        private Animator animator;
        private Coroutine attackCoroutine;

        public void Initialize(Animator spriteAnimator, Controller controller)
        {
            animator = spriteAnimator;
            enemyController = controller;
            enemyController.OnFlipRight += flipSwordSpawn;
        }

        private void Awake()
        {
            hitboxToAttack.OnPlayerInReach += startAttack;
            hitboxToAttack.OnPlayerOutOfReach += stopAttack;
            projectileSpawn = transform.Find("SwordSpawn");

            originalProjSpawnPos = projectileSpawn.localPosition;
        }

        private void OnDestroy()
        {
            enemyController.OnFlipRight -= flipSwordSpawn;
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

        private void flipSwordSpawn(bool onRightSide)
        {
            if (onRightSide)
                projectileSpawn.localPosition = new Vector3(0.244f, 0.035f, 0f);
            else
                projectileSpawn.localPosition = originalProjSpawnPos;
        }

        private IEnumerator distanceAttack()
        {
            while (inRange)
            {
                animator.SetBool("isAttacking", true);

                yield return new WaitForSeconds(animationTime - animationTimeOffset);

                Instantiate(projectile.gameObject, projectileSpawn.position, Quaternion.identity);

                yield return new WaitForSeconds(animationTimeOffset);

                animator.SetBool("isAttacking", false);

                yield return new WaitForSeconds(cooldownPerAttack);
            }

            attackCoroutine = null;
        }
    }
}