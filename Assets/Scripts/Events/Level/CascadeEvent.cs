using UnityEngine;

namespace Scripts.Events.Level
{
    public class CascadeEvent : MonoBehaviour
    {
        private BoxCollider2D leftWall;
        private Transform bottomWall;

        private void Awake()
        {
            leftWall = transform.parent.Find("Cascade_Left").GetComponent<BoxCollider2D>();
            bottomWall = transform.parent.Find("Cascade_Bottom");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Player"))
            {
                leftWall.enabled = true;
                Destroy(bottomWall.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
