using Scripts.Enemies.Common;
using Scripts.Events.Level;
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

        public string levelPart = "One";

        private Transform player;
        private Rigidbody2D rb;

        private bool inRange = false;
        private bool shouldMove = false;
        private bool isAttacking = false;

        [SerializeField]
        private float playerDistance = 0f;

        [SerializeField]
        private float minimumAvoidDistance = 3.5f;

        [SerializeField]
        private float movementSpeed = 1.5f;
        [SerializeField]
        private float cooldownPerAttack = 2.5f;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();

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
                Destroy(gameObject);
        }

        private void Update()
        {
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

        private IEnumerator attack()
        {
            isAttacking = true;
            Instantiate(gunProyectile.gameObject, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(cooldownPerAttack);
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
    }
}
