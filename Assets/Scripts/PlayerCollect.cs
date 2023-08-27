using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // interact with collectables
    private void OnTriggerEnter2D(Collider2D item)
    {
        if (item.gameObject.GetComponent<Stars>() && !item.gameObject.GetComponent<Stars>().isDone) {
            item.gameObject.GetComponent<Stars>().isDone = true;
            AudioManager.Instance.PlaySFX("Collect");
            Score.instance.ChangeScore(item.GetComponent<Stars>().starValue);
            Destroy(item.gameObject);
        }
    }
}
