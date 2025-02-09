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
        private BoxCollider2D tongueHitbox;
        [SerializeField]
        private SpriteRenderer tongueSprite;
        [SerializeField]
        private SpriteRenderer weakpointSprite;

        private int openDelay = 1;
        private int closeDelay = 1;

        private bool isAttacking = false;
        private bool canAttack = false;

        private void Awake()
        {
            seashellHitbox = GetComponent<CircleCollider2D>();
            visionRange = transform.parent.Find("VisionRange").GetComponent<VisionRange>();
            tongueHitbox = transform.parent.Find("Tongue").Find("Logic").GetComponent<BoxCollider2D>();
            tongueSprite = transform.parent.Find("Tongue").Find("Logic").Find("Sprite").GetComponent<SpriteRenderer>();
            weakpointSprite = transform.parent.Find("Sprite_Revealed").GetComponent<SpriteRenderer>();

            visionRange.OnPlayerInRange += setCanAttack;
        }

        private void Update()
        {
            if (!canAttack) return;

            if(!isAttacking)
                StartCoroutine(attackSequence());
        }

        private void setCanAttack(bool status)
            => canAttack = status;

        private IEnumerator attackSequence()
        {
            isAttacking = true;
            weakpointSprite.enabled = true;
            seashellHitbox.enabled = true;

            OnVulnerable?.Invoke(true);

            yield return new WaitForSeconds(openDelay);

            tongueSprite.enabled = true;
            tongueHitbox.enabled = true;

            yield return new WaitForSeconds(closeDelay);

            OnVulnerable?.Invoke(false);

            tongueSprite.enabled = false;
            tongueHitbox.enabled = false;
            weakpointSprite.enabled = false;
            seashellHitbox.enabled = false;

            yield return new WaitForSeconds(closeDelay);

            isAttacking = false;
        }
    }
}