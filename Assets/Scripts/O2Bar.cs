using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class O2Bar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxOxygen(float o2Limit)
    {
        slider.maxValue = o2Limit;
        slider.value = o2Limit;
    }

    public void SetCurrentOxygen(float o2)
    {
        slider.value = o2;
    }
}
