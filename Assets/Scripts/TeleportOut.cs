using UnityEngine;

public class TeleportOut : MonoBehaviour {
    private const float Force = 5f;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<PlayerMovement>()) {
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -1f) * Force, ForceMode2D.Impulse);
        }
    }
}