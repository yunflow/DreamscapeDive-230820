using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    public enum State {
        Normal,
        CanJump,
        CanHook,
        IsReverse,
    }

    [SerializeField] private State playerState;
    [SerializeField] private float groundSpeed; // 地面左右移动速度
    [SerializeField] private float airSpeed; // 空中左右移动速度
    [SerializeField] private float jumpForce; // 跳跃高度
    [SerializeField] private float airGravity = 0.2f; // 在空中的重力调整值，默认地面重力为1
    [SerializeField] private NewHook hook;
    [SerializeField] private GameObject hookBackground;
    [SerializeField] private Transform playerImage;

    [HideInInspector] public bool allowMove = true;
    private bool isOnPlanet;
    private bool canJump;
    private bool canHook;
    private bool isReverse;
    public bool isMoving;

    private Camera mainCamera;
    private Rigidbody2D rb;
    private Vector2 rawMoveInput;
    private CapsuleCollider2D footCollider;
    private PlayerDeath playerDeath;
    private float imageLocalX;


    // Unity gameplay
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        footCollider = GetComponent<CapsuleCollider2D>();
        playerState = State.Normal;
        playerDeath = GetComponent<PlayerDeath>();
    }

    private void Start() {
        mainCamera = Camera.main;
        hook.gameObject.SetActive(false);
        hookBackground.SetActive(false);
        imageLocalX = playerImage.localScale.x;
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
        if (other.gameObject.GetComponent<Planet>()) {
            Planet.State planetState = other.gameObject.GetComponent<Planet>().planetState;

            switch (planetState) {
                default:
                case Planet.State.Hook:
                    playerState = State.CanHook;
                    break;
                case Planet.State.Normal:
                    playerState = State.Normal;
                    break;
                case Planet.State.Jump:
                    playerState = State.CanJump;
                    break;
                case Planet.State.Reverse:
                    playerState = State.IsReverse;
                    break;
            }
        }

        if (other.gameObject.GetComponent<Obstacle>()) {
            AudioManager.Instance.PlaySFX("HitOnStone");
            playerDeath.GameOver();
        }
    }

    private void OnMove(InputValue value) {
        rawMoveInput = value.Get<Vector2>();
    }

    // 普通左右移动，根据是否在planet上改变速度
    private void NormalWalking() {
        if (!allowMove) return;

        if (isReverse) {
            rb.velocity = isOnPlanet
                ? new Vector2(-rawMoveInput.x * groundSpeed, rb.velocity.y)
                : new Vector2(-rawMoveInput.x * airSpeed, rb.velocity.y);
        }
        else {
            rb.velocity = isOnPlanet
                ? new Vector2(rawMoveInput.x * groundSpeed, rb.velocity.y)
                : new Vector2(rawMoveInput.x * airSpeed, rb.velocity.y);
        }

        isMoving = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        FlipIfMoveToLeft(isMoving);
    }

    private void FlipIfMoveToLeft(bool isRun) {
        if (isRun) {
            playerImage.localScale = new Vector2(-Mathf.Sign(rb.velocity.x) * imageLocalX, playerImage.localScale.y);
        }
    }

    // 根据角色状态切换能力bool
    private void StateSwitch() {
        switch (playerState) {
            default:
            case State.Normal:
                hook.gameObject.SetActive(false);
                hookBackground.SetActive(false);
                isReverse = false;
                canJump = false;
                canHook = false;
                break;

            case State.CanJump:
                hook.gameObject.SetActive(false);
                hookBackground.SetActive(false);
                isReverse = false;
                canJump = true;
                canHook = false;
                break;
            case State.CanHook:
                hook.gameObject.SetActive(true);
                hookBackground.SetActive(true);
                isReverse = false;
                canJump = false;
                canHook = true;
                break;

            case State.IsReverse:
                hook.gameObject.SetActive(false);
                hookBackground.SetActive(false);
                isReverse = true;
                canJump = false;
                canHook = false;
                break;
        }

        isOnPlanet = footCollider.IsTouchingLayers(LayerMask.GetMask("Planet"));
    }

    private void OnJump(InputValue value) {
        if (!canJump || playerDeath.IsGameOver) return;

        if (value.isPressed && isOnPlanet) {
            AudioManager.Instance.PlaySFX("Jump");
            rb.velocity = new Vector2(0f, jumpForce);
        }
    }

    private void OnHook(InputValue value) {
        if (!canHook || playerDeath.IsGameOver) return;

        if (value.isPressed) {
            hook.StartHook();
            playerState = State.Normal;
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

    public State GetState() {
        return playerState;
    }
}