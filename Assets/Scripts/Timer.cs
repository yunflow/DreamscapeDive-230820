using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour {
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private int maxTime = 30;

    private float currentTimeValue;
    private PlayerDeath playerDeath;

    private void Awake() {
        playerDeath = FindObjectOfType<PlayerDeath>();
    }

    private void Start() {
        timeText.text = maxTime.ToString();
        currentTimeValue = maxTime;
    }

    private void Update() {
        if (playerDeath.IsGameOver) return;

        SetTimer();
    }

    private void SetTimer() {
        // 如果时间到0，游戏结束
        if (Mathf.CeilToInt(currentTimeValue) == 0) {
            playerDeath.GameOver();
            return;
        }

        currentTimeValue -= Time.deltaTime;
        timeText.text = Mathf.CeilToInt(currentTimeValue).ToString();
    }
}