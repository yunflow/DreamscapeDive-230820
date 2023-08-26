using UnityEngine;

public class HookHead : MonoBehaviour {
    private bool isHooked;

    private float startTime = 0f;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Planet")) {
            isHooked = true;
            Destroy(gameObject, 1f);
        }
    }

    private void Update() {
        if (!isHooked) {
            startTime += Time.deltaTime;
            transform.Translate(Vector3.right * (Time.deltaTime * 18f));
            if (startTime > 0.3f) {
                Destroy(gameObject);
            }
        }
    }
}