using Scripts.Enemies.Common;
using Scripts.Events.Level;
using Scripts.Events.Player;
using System.Collections;
using UnityEngine;

namespace Scripts.Enemies.Fish_Gun
{
    public class Controller : MonoBehaviour
    {
        [SerializeField]
        private VisionInRange hitboxToAttack;
        [SerializeField]
        private Transform gunProyectile;
        private Animator animator;
        private SpriteRenderer spriteRenderer;

        private Transform gunSpawnpoint;
        private Vector3 rightGunSpawnpointPos = new Vector3(0.78f, -0.089f, 0f);
        private Vector3 originalGunSpawnpoint;

        public string levelPart = "One";

        private Transform player;
        private Rigidbody2D rb;

        private bool inRange = false;
        private bool shouldMove = false;
        private bool isAttacking = false;
        private bool isFacingRight = false;

        [SerializeField]
        private float playerDistance = 0f;

        [SerializeField]
        private float minimumAvoidDistance = 3.5f;

        [SerializeField]
        private float movementSpeed = 1.5f;
        [SerializeField]
        private float cooldownPerAttack = 2f;
        private float animationDelay = 0.55f;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = transform.Find("Sprite").GetComponent<Animator>();
            gunSpawnpoint = transform.Find("GunSpawn");
            spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
            originalGunSpawnpoint = gunSpawnpoint.localPosition;

            hitboxToAttack.OnPlayerInReach += shouldAttack;
            hitboxToAttack.OnPlayerOutOfReach += shouldStopAttack;

            player = GameObject.FindGameObjectWithTag("Player").transform;

            LevelOneEvents.OnPartFinished += autoDestroy;
        }

        private void OnDestroy()
        {
            LevelOneEvents.OnPartFinished -= autoDestroy;
        }

        private void autoDestroy(string level)
        {
            if (level == levelPart)
                transform.parent.gameObject.SetActive(false);
        }

        private void Update()
        {
            float direction = PlayerTracker.Instance.PlayerPosition.x - transform.position.x;

            if (direction < 0 && isFacingRight)
            {
                flip();
                gunSpawnpoint.localPosition = originalGunSpawnpoint;
            }
            else if (direction > 0 && !isFacingRight)
            {
                flip();
                gunSpawnpoint.localPosition = rightGunSpawnpointPos;
            }

            if (inRange)
            {
                playerDistance = Vector3.Distance(player.position, transform.position);

                if (playerDistance < minimumAvoidDistance)
                    shouldMove = true;
                else
                    shouldMove = false;

                if(!isAttacking)
                    StartCoroutine(attack());
            }
        }

        private void FixedUpdate()
        {
            if (shouldMove)
                move();
        }

        private void flip()
        {
            isFacingRight = !isFacingRight;

            spriteRenderer.flipX = isFacingRight;
        }

        private IEnumerator attack()
        {
            isAttacking = true;
            animator.SetBool("isAttacking", true);

            yield return new WaitForSeconds(animationDelay);

            Instantiate(gunProyectile.gameObject, gunSpawnpoint.position, Quaternion.identity);

            yield return new WaitForSeconds(cooldownPerAttack);

            animator.SetBool("isAttacking", false);
            isAttacking = false;
        }

        private void shouldStopAttack()
        {
            shouldMove = false;
            inRange = false;
            isAttacking = false;
            StopAllCoroutines();
            rb.linearVelocity = Vector2.zero;
            playerDistance = 0f;
        }

        private void shouldAttack()
        {
            inRange = true;
        }

        private void move()
        {
            var direction = (player.position - transform.position).normalized;

            rb.MovePosition(transform.position + (-direction) * movementSpeed * Time.fixedDeltaTime);
        }

        private void OnDisable()
            => StopAllCoroutines();
    }
}
