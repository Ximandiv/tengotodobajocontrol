using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Enemies.Fish_Sword
{
    public class Movement : MonoBehaviour
    {
        public List<Transform> TravelPoints = new();

        [SerializeField]
        private int speed = 4;
        [SerializeField]
        private int currentIndex = 0;

        private Rigidbody2D rb;
        private bool isMoving = true;

        public void Initialize(Rigidbody2D enemyRb)
        {
            rb = enemyRb;
        }

        private void FixedUpdate()
        {
            if (isMoving && currentIndex < TravelPoints.Count)
            {
                moveToNextPoint();
            }
        }

        private void moveToNextPoint()
        {
            Transform nextPoint = TravelPoints[currentIndex];

            Vector2 direction = ((Vector2)nextPoint.position - rb.position).normalized;

            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);

            if (Vector2.Distance(rb.position, nextPoint.position) <= 0.1f)
            {
                currentIndex++;
                if (currentIndex >= TravelPoints.Count)
                    currentIndex = 0;
            }
        }
    }
}
