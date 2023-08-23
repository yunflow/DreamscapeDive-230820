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
        if (item.gameObject.CompareTag("CollectableStars"))
            Destroy(item.gameObject);
    }
}
