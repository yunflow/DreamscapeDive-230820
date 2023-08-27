using TMPro;
using UnityEngine;

public class FInalTimeDisplay : MonoBehaviour {
    private TMP_Text finalTimeText;

    private void Awake() {
        finalTimeText = GetComponent<TMP_Text>();
    }

    private void Start() {
        int finalTime = Mathf.FloorToInt(TotleTimeKeeper.Instance.TotalTime);
        finalTimeText.text = $"You Final Time:\n{finalTime} sec!\nKeep Trying!";
    }
}