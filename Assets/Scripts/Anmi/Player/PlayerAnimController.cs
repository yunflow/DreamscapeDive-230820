using UnityEngine;

public class PlayerAnimController : MonoBehaviour {
    private Animator animator;
    private PlayerMovement playerMovement;

    private void Awake() {
        animator = GetComponent<Animator>();
        playerMovement = transform.parent.GetComponent<PlayerMovement>();
    }

    private void FixedUpdate() {
        animator.SetBool("isMove", playerMovement.isMoving);
        animator.SetBool("Jump", playerMovement.GetState() == PlayerMovement.State.CanJump);
        animator.SetBool("Hook", playerMovement.GetState() == PlayerMovement.State.CanHook);
    }
}