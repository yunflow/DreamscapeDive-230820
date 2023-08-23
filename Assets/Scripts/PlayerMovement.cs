using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    private enum State {
        Normal,
        CanJump,
        CanHook,
    }

    [SerializeField] private State playerState;
    [SerializeField] private float groundSpeed; // 地面左右移动速度
    [SerializeField] private float airSpeed; // 空中左右移动速度
    [SerializeField] private float jumpForce; // 跳跃高度
    [SerializeField] private float airGravity = 0.2f; // 在空中的重力调整值，默认地面重力为1

    private bool isOnPlanet;
    private bool canJump;
    private bool canHook;

    private Camera mainCamera;
    private Rigidbody2D rb;
    private Vector2 rawMoveInput;
    private CapsuleCollider2D footCollider;
    private PlayerDeath playerDeath;
    private NewHook hook;


    // Unity gameplay
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        footCollider = GetComponent<CapsuleCollider2D>();
        playerState = State.Normal;
        playerDeath = GetComponent<PlayerDeath>();
        hook = FindObjectOfType<NewHook>();
    }

    private void Start() {
        mainCamera = Camera.main;
        hook.gameObject.SetActive(false);
    }

    private void Update() {
        if (playerDeath.IsGameOver) return;

        StateSwitch();
        DetectingGravity();
        NormalWalking();
        KeepPlayerOnScreen();
    }

    // 根据所在星球切换角色状态
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.GetComponent<JumpPlanet>()) {
            playerState = State.CanJump;
        }
        else if (other.gameObject.GetComponent<HookPlanet>()) {
            hook.gameObject.SetActive(true);
            playerState = State.CanHook;
        }
        else if (other.gameObject.GetComponent<NormalPlanet>()) {
            playerState = State.Normal;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        hook.gameObject.SetActive(false);
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

    // 根据角色状态切换能力bool
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
        if (!canJump || playerDeath.IsGameOver) return;

        if (value.isPressed && isOnPlanet) {
            rb.velocity = new Vector2(0f, jumpForce);
        }
    }

    private void OnHook(InputValue value) {
        if (!canHook || playerDeath.IsGameOver) return;

        if (value.isPressed && isOnPlanet) {
            hook.StartHook();
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

        transform.position = newPos;
    }
}