using UnityEngine;

public class NewHook : MonoBehaviour {
    [SerializeField] private float rotationSpeed = 150f; // 发射点自转速度
    [SerializeField] private GameObject hookBullet;
    [SerializeField] private GameObject hookHeadSprite;
    [SerializeField] private Transform spawnPoint;

    private Transform parentPos; // 发射点围绕角色转的坐标 （角色坐标）

    private void Start() {
        parentPos = transform.parent;
    }

    private void Update() {
        transform.RotateAround(parentPos.position, Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    // 角色按E时调用这个方法
    public void StartHook() {
        Instantiate(hookBullet, spawnPoint.position, spawnPoint.rotation);
        Instantiate(hookHeadSprite, spawnPoint.position, spawnPoint.rotation);
    }
}