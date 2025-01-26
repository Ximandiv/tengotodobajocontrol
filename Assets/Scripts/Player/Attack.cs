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

        private bool isAttacking = false;

        private void Update()
        {
            if (Input.GetMouseButtonDown(1) && !isAttacking)
                StartCoroutine(shoot());
        }

        private IEnumerator shoot()
        {
            isAttacking = true;
            var newProjectile = Instantiate(projectile.gameObject, transform.position, Quaternion.identity);

            bool isFlipped = spriteRenderer.flipX;
            newProjectile.GetComponent<BubbleProjectile>().InitializeDirection(isFlipped);

            yield return new WaitForSeconds(10);
            isAttacking = false;
        }
    }
}