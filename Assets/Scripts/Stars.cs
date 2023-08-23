using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public int starValue = 1;

    private void OnTriggerEnter2D(Collider2D item)
    {
        if (item.gameObject.CompareTag("Player"))
            Score.instance.ChangeScore(starValue);
    }
}
