using Scripts.Events.Player;
using Scripts.Projectiles.Player;
using System.Collections;
using UnityEngine;

namespace Scripts.Player
{
    public class Attack : MonoBehaviour
    {
        // Mode 0 absorbs. 1 = Hedgehog. 2 = Sword. 3 = Gun
        public int mode = 0;
        public Transform projectile;

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        public bool isAttacking = false;

        private void Update()
        {
            if ((Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space)) 
                && !isAttacking)
                StartCoroutine(shoot());
        }

        private IEnumerator shoot()
        {
            isAttacking = true;
            PlayerEvents.InvokePlayerShootingBubbles(true);

            yield return new WaitForSeconds(0.5f);

            PlayerEvents.InvokePlayerShootingBubbles(false);

            var newProjectile = Instantiate(projectile.gameObject, transform.position, Quaternion.identity);

            bool isFlipped = spriteRenderer.flipX;
            newProjectile.GetComponent<BubbleProjectile>().InitializeDirection(isFlipped);

            yield return new WaitForSeconds(5);
            isAttacking = false;
        }
    }
}