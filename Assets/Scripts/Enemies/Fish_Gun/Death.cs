using Scripts.Projectiles.Player;
using System.Collections;
using UnityEngine;

namespace Scripts.Enemies.Fish_Gun
{
    public class Death : MonoBehaviour
    {
        [SerializeField]
        private Controller controller;
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private float animationDelay = 0f;

        private bool hasStarted = false;

        private void Awake()
        {
            controller = transform.parent.GetComponent<Controller>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (hasStarted) return;

            if(collision.CompareTag("PlayerProjectile"))
            {
                hasStarted = true;

                controller.enabled = false;
                collision.transform.Find("Health").GetComponent<Player_ProjectileDeath>().AutoDestroy();
                StartCoroutine(startSequence());
            }
        }

        private IEnumerator startSequence()
        {
            animator.SetBool("isDead", true);
            yield return new WaitForSeconds(animationDelay);
            Destroy(transform.parent.gameObject);
        }
    }
}