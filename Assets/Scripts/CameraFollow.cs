using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField] private float lower = -1f; // 相机能移动到的最低点
    [SerializeField] private float upper = 10f; // 相机能移动到的最高点
    [SerializeField] private Transform player;
    [SerializeField] private float yOffset = 1f;
    [SerializeField] private float smoothTime = 0.3f; // 相机延迟跟随时间

    private Vector3 cameraPosition;
    private Vector3 velocity = Vector3.one;

    private void Start() {
        cameraPosition = transform.position;
    }

    private void LateUpdate() {
        cameraPosition.y = player.position.y + yOffset;
        cameraPosition.y = Mathf.Clamp(cameraPosition.y, lower, upper);
        transform.position = Vector3.SmoothDamp(transform.position, cameraPosition, ref velocity, smoothTime);
    }
}