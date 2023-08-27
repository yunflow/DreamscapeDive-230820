using TMPro;
using UnityEngine;

public class FInalTimeDisplay : MonoBehaviour {
    private TMP_Text finalTimeText;

    private void Awake() {
        finalTimeText = GetComponent<TMP_Text>();
    }

    private void Start() {
        finalTimeText.text = $"You Final Time: {Mathf.FloorToInt(TotleTimeKeeper.Instance.TotalTime)}";
    }
}