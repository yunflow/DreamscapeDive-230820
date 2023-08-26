using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBar : MonoBehaviour
{
    public Slider slider;

    public void SetGoal(int goal)
    {
        slider.maxValue = goal;
        slider.value = 0;
    }

    public void SetScore(int score)
    {
        slider.value = score;
    }
}
