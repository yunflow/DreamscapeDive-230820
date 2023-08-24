using UnityEngine;

public class Teleport : MonoBehaviour {
    [SerializeField] private Transform portOut;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<PlayerMovement>()) {
            other.gameObject.transform.position = portOut.position;
        }
    }
}