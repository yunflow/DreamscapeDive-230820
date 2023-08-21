using UnityEngine;

public class FinishingLine : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<PlayerMovement>()) {
            DetectingStar();
        }
    }

    private void DetectingStar() {
        // 玩家摸到终点，检查星星是否够，判断游戏是否成功
        Debug.Log("检测星星数量");
    }
}