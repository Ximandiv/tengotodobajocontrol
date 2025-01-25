using Scripts.Events.Cutscenes;
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

        #region Unity API Methods

        private void Awake()
        {
            if(!gameStatus.StartCutsceneEnd)
                canMove = false;

            rb = GetComponent<Rigidbody2D>();
            CutsceneEvents.OnStart += onFinishedInitCutscenes;
        }

        private void Update()
        {
            if (!canMove) return;

            getInput();

            isMoving = isCurrentlyMoving();
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
            var direction = movementInput.normalized;

            rb.linearVelocity = direction * movementSpeed;
        }

        private void stop()
            => rb.linearVelocity = Vector2.zero;

        private void getInput()
        {
            movementInput.x = Input.GetAxisRaw("Horizontal");
            movementInput.y = Input.GetAxisRaw("Vertical");
        }

        private void setCanMove(bool newStatus)
            => canMove = newStatus;

        private void onFinishedInitCutscenes()
        {
            canMove = true;
            CutsceneEvents.OnStart -= onFinishedInitCutscenes;
        }

        private bool isCurrentlyMoving()
        {
            var sqrMagnitudeInput = (movementInput.normalized).sqrMagnitude;

            return sqrMagnitudeInput > MINIMUMINPUTVALUE;
        }

        #endregion
    }
}