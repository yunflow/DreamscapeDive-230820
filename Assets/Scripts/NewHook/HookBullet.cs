using System.Collections;
using UnityEngine;

public class HookBullet : MonoBehaviour {
    [SerializeField] private float growTime = 1f;
    [SerializeField] private float hookRange = 10f;

    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D capsuleCollider2D;

    private bool isGrowing = true;
    private bool isHooked;
    private bool hookProcessing;

    [HideInInspector] public Vector2 playerTargetPos;
    private GameObject player;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    private void Start() {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        StartCoroutine(IncreaseHookLengthRoutine());
    }

    private void Update() {
        if (isHooked && !hookProcessing) {
            hookProcessing = true;
            StartCoroutine(HookProcessing());
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Planet")) {
            isGrowing = false;
            isHooked = true;

            Vector2 dir = (player.transform.position - other.transform.position).normalized;
            playerTargetPos = (Vector2)other.transform.position + (new Vector2(0.5f, 0.5f) * dir);
        }
    }

    private IEnumerator HookProcessing() {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        rb.isKinematic = true;
        playerMovement.allowMove = false;

        float timePassed = 0f;
        while (timePassed < 1f) {
            timePassed += Time.deltaTime;
            player.transform.position = Vector2.Lerp(player.transform.position, playerTargetPos, 3 * Time.deltaTime);

            yield return null;
        }

        rb.isKinematic = false;
        playerMovement.allowMove = true;
        Destroy(gameObject);
    }

    private IEnumerator IncreaseHookLengthRoutine() {
        float timePassed = 0f;
        while (spriteRenderer.size.x < hookRange && isGrowing) {
            timePassed += Time.deltaTime;
            float linearT = timePassed / growTime;

            // sprite
            spriteRenderer.size = new Vector2(Mathf.Lerp(1f, hookRange, linearT), 1f);

            // collider
            capsuleCollider2D.size = new Vector2(Mathf.Lerp(1f, hookRange, linearT), capsuleCollider2D.size.y);
            capsuleCollider2D.offset =
                new Vector2((Mathf.Lerp(1f, hookRange, linearT)) / 2, capsuleCollider2D.offset.y);

            yield return null;
        }

        if (!isHooked) {
            Destroy(gameObject);
        }
    }
}