using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static Score instance;
    public TextMeshProUGUI text;
    [SerializeField] private int goal = 2;

    int score;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
    }

    private void Update()
    {
        WinCondition();
    }

    public void ChangeScore(int value)
    {
        if (score == goal)
            text.text = "WIN";
        else
        {
            score += value;
            text.text = "X" + score.ToString();
        }
    }

    // win condition, end game when achieve a certian score
    private void WinCondition()
    {
        if (score == goal) {
            text.text = "WIN";
        }
    }
}
