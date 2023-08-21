using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour {
    [SerializeField] private GameObject gameOverImage;

    public bool IsGameOver { get; private set; }

    private Camera mainCamera;


    private void Start() {
        mainCamera = Camera.main;
    }

    private void LateUpdate() {
        if (IsGameOver) return;

        DetectingDeath();
    }

    // 检查玩家掉落到屏幕下方，游戏结束
    private void DetectingDeath() {
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(transform.position);

        if (viewportPos.y < 0) {
            GameOver();
        }
    }

    public void GameOver() {
        IsGameOver = true;
        Debug.Log("GameOver");
        gameOverImage.SetActive(true);
        Invoke(nameof(LoadScene), 2f);
    }

    private void LoadScene() {
        SceneManager.LoadScene(0);
    }
}