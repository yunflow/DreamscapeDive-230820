using UnityEngine;

public class Teleport : MonoBehaviour {
    [SerializeField] private Transform portOut;

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.GetComponent<PlayerMovement>()) {
            AudioManager.Instance.PlaySFX("Teleport");
            other.gameObject.transform.position = portOut.position;
        }
    }
}