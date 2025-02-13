using System;
using UnityEngine;

namespace Scripts.Events.Level
{
    public class CascadeEvent : MonoBehaviour
    {
        public static event Action<bool> OnCascade;

        private BoxCollider2D leftWall;
        private Transform bottomWall;

        public static void InvokeCascade(bool status)
            => OnCascade?.Invoke(status);

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
                transform.GetComponent<BoxCollider2D>().enabled = false;
                OnCascade?.Invoke(true);
            }
        }
    }
}
