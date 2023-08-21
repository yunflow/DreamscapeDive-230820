using UnityEngine;

public class Teleport : MonoBehaviour {
    [SerializeField] private Transform portOut;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<PlayerMovement>()) {
            other.gameObject.transform.position = portOut.position;
            // 待解决：如果出口在相机下方，会直接判定游戏失败
        }
    }
}