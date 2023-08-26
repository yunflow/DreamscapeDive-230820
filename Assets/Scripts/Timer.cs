using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour {
    [SerializeField] private float maxTime = 30;

    public O2Bar o2Bar;
    private float currentTimeValue;
    private PlayerDeath playerDeath;

    private void Awake() {
        playerDeath = FindObjectOfType<PlayerDeath>();
    }

    private void Start() {
        currentTimeValue = maxTime;
        o2Bar.SetMaxOxygen(maxTime);
        o2Bar.SetCurrentOxygen(currentTimeValue);
    }

    private void Update() {
        if (playerDeath.IsGameOver) return;

        SetTimer();
    }

    private void SetTimer() {
        // 如果时间到0，游戏结束
        if (currentTimeValue <= 0) {
            playerDeath.GameOver();
            return;
        }

        currentTimeValue -= Time.deltaTime;
        o2Bar.SetCurrentOxygen(currentTimeValue);
    }
}