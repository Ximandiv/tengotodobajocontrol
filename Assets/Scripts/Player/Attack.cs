using Scripts.Events.Player;
using Scripts.Projectiles.Player;
using System.Collections;
using UnityEngine;

namespace Scripts.Player
{
    public class Attack : MonoBehaviour
    {
        // Mode 0 absorbs. 1 = Hedgehog. 2 = Sword. 3 = Gun
        [SerializeField]
        private int mode = 0;
        [SerializeField]
        private Transform bubbleProjectile;
        [SerializeField]
        private Transform swordProjectile;

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        public bool isAttacking = false;

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                if (mode == 0)
                    mode = 1;
                else
                    mode = 0;
            }

            if ((Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space))
                && !isAttacking)
                StartCoroutine(shoot());
        }

        private GameObject getProjectileOnMode()
        {
            switch(mode)
            {
                case 0:
                    return bubbleProjectile.gameObject;
                case 1:
                    return swordProjectile.gameObject;
                default:
                    return null;
            }
        }

        private void initializeProjectileDir(GameObject newProjectile, bool isFlipped)
        {
            switch(mode)
            {
                case 0:
                    newProjectile.GetComponent<Player_BubbleProjectile>().InitializeDirection(isFlipped);
                    break;
                case 1:
                    newProjectile.GetComponent<Player_SwordProjectile>().InitializeDirection(isFlipped);
                    break;
            }
        }

        private IEnumerator shoot()
        {
            isAttacking = true;
            PlayerEvents.InvokePlayerShootingBubbles(true);

            yield return new WaitForSeconds(0.5f);

            PlayerEvents.InvokePlayerShootingBubbles(false);

            GameObject projectile = getProjectileOnMode();
            var newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
            initializeProjectileDir(newProjectile, spriteRenderer.flipX);

            yield return new WaitForSeconds(5);
            isAttacking = false;
        }
    }
}