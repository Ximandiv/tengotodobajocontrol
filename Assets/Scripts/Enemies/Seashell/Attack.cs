using Scripts.Projectiles.Player;
using System;
using System.Collections;
using UnityEngine;

namespace Scripts.Enemies.Seashell
{
    public class Attack : MonoBehaviour
    {
        public event Action<bool> OnVulnerable;

        private VisionRange visionRange;

        [SerializeField]
        private CircleCollider2D seashellHitbox;
        [SerializeField]
        private PolygonCollider2D tongueHitbox;
        [SerializeField]
        private SpriteRenderer tongueSprite;
        [SerializeField]
        private SpriteRenderer weakpointSprite;
        [SerializeField]
        private SpriteRenderer shellSprite;
        [SerializeField]
        private TongueAttack tongueAttack;
        [SerializeField]
        private Shell_Death death;

        [SerializeField]
        private Animator shellAnimator;
        [SerializeField]
        private Animator tongueAnimator;

        private int openDelay = 1;
        private float stayOpenDelay = 1.5f;
        private int closeDelay = 2;

        private bool isAttacking = false;
        private bool canAttack = false;
        private bool isFacingRight = false;

        private void Awake()
        {
            seashellHitbox = GetComponent<CircleCollider2D>();
            visionRange = transform.parent.Find("VisionRange").GetComponent<VisionRange>();
            tongueHitbox = transform.parent.Find("Tongue").Find("Logic").GetComponent<PolygonCollider2D>();
            tongueSprite = transform.parent.Find("Tongue").Find("Logic").Find("Sprite").GetComponent<SpriteRenderer>();
            weakpointSprite = transform.parent.Find("Sprite_Revealed").GetComponent<SpriteRenderer>();
            shellAnimator = transform.parent.Find("Sprite_Shell").GetComponent<Animator>();
            tongueAnimator = transform.parent.Find("Tongue").Find("Logic").Find("Sprite").GetComponent<Animator>();
            shellSprite = transform.parent.Find("Sprite_Shell").GetComponent<SpriteRenderer>();
            tongueAttack = transform.parent.Find("Tongue").Find("Logic").GetComponent<TongueAttack>();
            death = transform.parent.Find("Sprite_Shell").GetComponent<Shell_Death>();

            visionRange.OnPlayerInRange += setCanAttack;
        }

        private void Update()
        {
            if (!canAttack) return;

            float direction = PlayerTracker.Instance.PlayerPosition.x - transform.position.x;

            if (direction < 0 && isFacingRight)
            {
                flip();
                tongueAttack.transform.localRotation = Quaternion.Euler(0, 0, 0);
                tongueAttack.FlipAngleOffset(-15);
            }
            else if (direction > 0 && !isFacingRight)
            {
                flip();
                tongueAttack.transform.localRotation = Quaternion.Euler(-180, 0, 0);
                tongueAttack.FlipAngleOffset(15);
            }

            if (!isAttacking)
                StartCoroutine(attackSequence());
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("PlayerProjectile")) return;

            visionRange.enabled = false;
            tongueAttack.enabled = false;
            seashellHitbox.enabled = false;

            StopAllCoroutines();

            collision.transform.Find("Health").GetComponent<Player_ProjectileDeath>().AutoDestroy();

            death.StartSequence();
        }

        private void flip()
        {
            isFacingRight = !isFacingRight;

            shellSprite.flipX = isFacingRight;
        }

        private void setCanAttack(bool status)
            => canAttack = status;

        private IEnumerator attackSequence()
        {
            isAttacking = true;
            weakpointSprite.enabled = true;
            seashellHitbox.enabled = true;

            shellAnimator.SetBool("isAttacking", true);
            shellAnimator.SetBool("isInRange", true);

            OnVulnerable?.Invoke(true);

            yield return new WaitForSeconds(openDelay);

            tongueSprite.enabled = true;
            tongueHitbox.enabled = true;

            tongueAnimator.SetBool("isAttacking", true);

            yield return new WaitForSeconds(stayOpenDelay);

            OnVulnerable?.Invoke(false);

            shellAnimator.SetBool("isAttacking", false);
            tongueAnimator.SetBool("isAttacking", false);

            tongueSprite.enabled = false;
            tongueHitbox.enabled = false;
            weakpointSprite.enabled = false;
            seashellHitbox.enabled = false;

            yield return new WaitForSeconds(closeDelay);

            shellAnimator.SetBool("isInRange", false);

            isAttacking = false;
        }
    }
}