using Scripts.Events.Level;
using UnityEngine;

namespace Scripts.Level
{
    public class PushPlayer : MonoBehaviour
    {
        [SerializeField]
        private Vector2 impulseDirection = Vector2.zero;
        [SerializeField]
        private float impulseForce = 10f;
        [SerializeField]
        private bool playerIsInZone = false;

        private Rigidbody2D playerRb;

        private void Start()
        {
            playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
            CascadeEvent.OnCascade += impulseOnStatus;
        }

        private void OnDestroy()
        {
            CascadeEvent.OnCascade -= impulseOnStatus;
        }

        private void FixedUpdate()
        {
            if (!playerIsInZone) return;

            playerRb.AddForce(impulseDirection.normalized * impulseForce, ForceMode2D.Force);
        }

        private void impulseOnStatus(bool status)
        {
            if(status)
            {
                playerIsInZone = true;
                return;
            }

            playerIsInZone = false;
            playerRb.linearVelocity = Vector2.zero;
            Destroy(gameObject);
        }
    }
}
