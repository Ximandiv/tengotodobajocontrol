using Scripts.Events.Level;
using UnityEngine;

namespace Scripts.Level
{
    public class OnHitOnWall : MonoBehaviour
    {
        [SerializeField]
        private Transform invisibleWall;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;

            invisibleWall.GetComponent<BoxCollider2D>().enabled = true;

            CascadeEvent.InvokeCascade(false);

            Destroy(gameObject);
        }
    }
}
