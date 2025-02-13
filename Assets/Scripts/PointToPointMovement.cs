using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class PointToPointMovement : MonoBehaviour
    {
        [SerializeField]
        private float speed = 10f;
        [SerializeField]
        private List<Transform> pointToMove = new List<Transform>();
        [SerializeField]
        private int currentIndex = 0;
        [SerializeField]
        private Transform currentPoint;

        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            currentPoint = pointToMove[currentIndex];
        }

        private void Update()
        {
            if (transform.position == currentPoint.position
                && currentIndex < pointToMove.Count - 1)
            {
                currentIndex += 1;
                currentPoint = pointToMove[currentIndex];
            }
        }

        private void FixedUpdate()
        {
            rb.MovePosition(Vector2.MoveTowards(transform.position, currentPoint.position, speed * Time.fixedDeltaTime));
        }
    }
}
