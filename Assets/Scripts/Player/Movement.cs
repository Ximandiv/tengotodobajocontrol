using Scripts.Events.Cutscenes;
using Scripts.Events.Player;
using UnityEngine;

namespace Scripts.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : MonoBehaviour
    {
        #region Private Variables

        #region Constants

        private float MINIMUMINPUTVALUE = 0.01f;

        #endregion

        #region Configuration

        [Header("Configuration")]

        [SerializeField]
        private float movementSpeed = 5.0f;

        [SerializeField]
        private Vector2 movementInput = Vector2.zero;

        [SerializeField]
        private Vector2 direction = Vector2.zero;

        [SerializeField]
        private GameStatus gameStatus;

        #endregion

        private Rigidbody2D rb;

        #region Status

        [Header("Status")]

        [SerializeField]
        private bool canMove = true;
        [SerializeField]
        private bool isMoving = false;

        #endregion

        #endregion

        public void Initialize(Rigidbody2D playerRb)
            => rb = playerRb;

        public void OnPlayerKilled()
        {
            canMove = false;
            stop();
        }

        public void OnFinishedInitCutscenes()
        {
            canMove = true;
        }

        #region Unity API Methods

        private void Awake()
        {
            if(!gameStatus.StartCutsceneEnd)
                canMove = false;

            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (!canMove) return;

            getInput();

            isMoving = isCurrentlyMoving();

            if(isMoving)
                PlayerEvents.InvokePlayerMoving(direction);
            else
                PlayerEvents.InvokePlayerMoving(Vector2.zero);
        }

        private void FixedUpdate()
        {
            if (isMoving)
                move();
            else
                stop();
        }

        #endregion

        #region Private Methods

        private void move()
        {
            direction = movementInput.normalized;

            rb.linearVelocity = direction * movementSpeed;
        }

        private void stop()
        {
            isMoving = false;
            rb.linearVelocity = Vector2.zero;
        }

        private void getInput()
        {
            movementInput.x = Input.GetAxisRaw("Horizontal");
            movementInput.y = Input.GetAxisRaw("Vertical");
        }

        private bool isCurrentlyMoving()
        {
            var sqrMagnitudeInput = (movementInput.normalized).sqrMagnitude;

            return sqrMagnitudeInput > MINIMUMINPUTVALUE;
        }

        #endregion
    }
}