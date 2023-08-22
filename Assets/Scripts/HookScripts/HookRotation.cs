using UnityEngine;

public class HookRotation : MonoBehaviour
{
    public Transform player; // 玩家的Transform
    public float revolutionSpeed = 50.0f; // 公转速度
    public bool shouldRotate = true; // 控制是否应用公转

    private void Update()
    {
        RotateAroundPlayer();
    }

    private void RotateAroundPlayer()
    {
        if (shouldRotate)
        {
            // 计算玩家的前进方向
            Vector3 playerForward = player.up;

            // 计算绕玩家公转
            float angle = Time.time * revolutionSpeed;
            Quaternion rotation = Quaternion.Euler(0, 0, -angle);
            Vector3 offset = rotation * playerForward;

            // 设置钩子的位置和旋转
            transform.position = player.position + offset;
            transform.up = offset;
        }
    }

    // 调用此方法暂停旋转
    public void PauseRotation()
    {
        shouldRotate = false;
    }

    // 调用此方法恢复旋转
    public void ResumeRotation()
    {
        shouldRotate = true;
    }
}
