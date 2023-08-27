using System;
using UnityEngine;

public class TotleTimeKeeper : MonoBehaviour {
    public static TotleTimeKeeper Instance;

    public float TotalTime { get; private set; }
    private bool IsProcessing { get; set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    private void Update() {
        if (IsProcessing) {
            TotalTime += Time.deltaTime;
            Debug.Log(TotalTime);
        }
    }

    public void StartTotalTime() {
        IsProcessing = true;
    }

    public void PauseTotalTime() {
        IsProcessing = false;
    }

    public void ResetTotalTime() {
        TotalTime = 0f;
    }
}