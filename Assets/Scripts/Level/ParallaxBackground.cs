using UnityEngine;

namespace Scripts.Level
{
    public class ParallaxBackground : MonoBehaviour
    {
        // The effect multiplier - lower numbers create a slower/more distant effect
        [Range(0f, 1f)]
        public float parallaxEffect = 0.5f;

        // Reference to the main camera
        private Camera mainCamera;

        // Starting position of the background
        private Vector3 startPosition;

        // How far the camera has moved from its initial position
        private Vector2 cameraStartPosition;

        void Start()
        {
            // Store initial positions
            startPosition = transform.position;
            mainCamera = Camera.main;
            cameraStartPosition = mainCamera.transform.position;
        }

        void Update()
        {
            // Calculate how far the camera has moved from its initial position
            Vector2 cameraDelta = (Vector2)mainCamera.transform.position - cameraStartPosition;

            // Calculate the parallax position
            // Objects with parallaxEffect = 1 will move exactly with the camera (no parallax)
            // Objects with parallaxEffect = 0 will stay completely still
            // Values in between create the parallax effect
            Vector3 newPosition = startPosition + new Vector3(
                cameraDelta.x * (1 - parallaxEffect),
                cameraDelta.y * (1 - parallaxEffect),
                transform.position.z
            );

            // Update the background position
            transform.position = newPosition;
        }
    }
}
