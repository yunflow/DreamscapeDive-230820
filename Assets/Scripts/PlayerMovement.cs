using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    private enum State {
        Normal,
        CanJump,
        CanHook,
    }

    [SerializeField] private State playerState;
    [SerializeField] private float groundSpeed;
    [SerializeField] private float airSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float airGravity;

    private bool isOnPlanet;
    private bool canJump;
    private bool canHook;

    private Camera mainCamera;
    private Rigidbody2D rb;
    private Vector2 rawMoveInput;
    private CapsuleCollider2D footCollider;


    // Unity gameplay
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        footCollider = GetComponent<CapsuleCollider2D>();
        playerState = State.Normal;
    }

    private void Start() {
        mainCamera = Camera.main;
    }

    private void Update() {
        StateSwitch();
        DetectingGravity();
        NormalWalking();
        KeepPlayerOnScreen();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.GetComponent<JumpPlanet>()) {
            playerState = State.CanJump;
        }
        else if (other.gameObject.GetComponent<HookPlanet>()) {
            playerState = State.CanHook;
        }
    }

    private void OnMove(InputValue value) {
        rawMoveInput = value.Get<Vector2>();
    }

    // 普通左右移动，根据是否在planet上改变速度
    private void NormalWalking() {
        rb.velocity = isOnPlanet
            ? new Vector2(rawMoveInput.x * groundSpeed, rb.velocity.y)
            : new Vector2(rawMoveInput.x * airSpeed, rb.velocity.y);
    }

    private void StateSwitch() {
        switch (playerState) {
            default:
            case State.Normal:
                canJump = false;
                canHook = false;
                break;

            case State.CanJump:
                canJump = true;
                canHook = false;
                break;
            case State.CanHook:
                canJump = false;
                canHook = true;
                break;
        }

        isOnPlanet = footCollider.IsTouchingLayers(LayerMask.GetMask("Planet"));
    }

    private void OnJump(InputValue value) {
        if (!canJump) return;
        if (value.isPressed && isOnPlanet) {
            rb.velocity = new Vector2(0f, jumpForce);
        }
    }

    private void DetectingGravity() {
        if (isOnPlanet) {
            rb.gravityScale = 1;
        }
        else {
            rb.gravityScale = airGravity;
        }
    }

    private void KeepPlayerOnScreen() {
        Vector3 newPos = transform.position;
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(newPos);

        newPos.x = viewportPos.x switch {
            > 1 => -newPos.x + 0.1f,
            < 0 => -newPos.x - 0.1f,
            _ => newPos.x
        };

        newPos.y = viewportPos.y switch {
            > 1 => -newPos.y + 0.1f,
            < 0 => -newPos.y - 0.1f,
            _ => newPos.y
        };

        transform.position = newPos;
    }
}