using Scripts.Events.Player;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        PlayerEvents.OnPlayerMoving += handleMovement;
        PlayerEvents.OnPlayerShootingBubbles += handleShootingBubbles;
    }

    private void OnDestroy()
    {
        PlayerEvents.OnPlayerMoving -= handleMovement;
        PlayerEvents.OnPlayerShootingBubbles -= handleShootingBubbles;
    }

    private void handleShootingBubbles(bool isShooting)
    {
        animator.SetBool("isShootingBubbles", isShooting);
    }

    private void handleMovement(Vector2 direction)
    {
        if(direction == Vector2.zero)
        {
            animator.SetBool("isMoving", false);
            return;
        }

        if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (direction.x > 0)
            spriteRenderer.flipX = false;

        animator.SetBool("isMoving", true);
    }
}
