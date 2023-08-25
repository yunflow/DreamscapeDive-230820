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
    }

    // detect death by touch the bottom of the map.
    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
            GameOver();
    }

    public void GameOver() {
        AudioManager.Instance.PlaySFX("Die");
        IsGameOver = true;
        Debug.Log("GameOver");
        gameOverImage.SetActive(true);
        Invoke(nameof(LoadScene), 2f);
    }

    private void LoadScene() {
        int level = LevelManager.Instance.GetCurrentLevelIndex();
        Debug.Log("Reloading: " + level);
        SceneManager.LoadScene(level);
    }
}