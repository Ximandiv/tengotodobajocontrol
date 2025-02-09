using UnityEngine;

namespace Scripts.Enemies.Seashell
{
    public class Health : MonoBehaviour
    {
        private bool isVulnerable = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!isVulnerable) return;

            if(collision.CompareTag("PlayerProjectile"))
                Destroy(transform.parent.gameObject);
        }
    }
}
