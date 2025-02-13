using System.Collections;
using UnityEngine;

namespace Scripts.Projectiles.Player
{
    public class Player_ProjectileDeath : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private Collider2D hitbox;
        [SerializeField]
        private Transform projectile;
        [SerializeField]
        private float animationDelay;

        private bool hasStarted = false;

        public void AutoDestroy()
        {
            if (hasStarted) return;

            hitbox.enabled = false;
            StartCoroutine(startSequence());
        }

        private IEnumerator startSequence()
        {
            hasStarted = true;
            animator.SetBool("isDead", true);

            yield return new WaitForSeconds(animationDelay);

            Destroy(projectile.gameObject);
        }
    }
}