using UnityEngine;

public class FinishingLine : MonoBehaviour {

    public Score score;
    [SerializeField] private int goal = 2;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<PlayerMovement>()) {
            DetectingStar();
        }
    }

    private void DetectingStar() {
        if (score.score >= goal)
        {
            Debug.Log("WIN!!!");
            NextLevel();
        }
            
    }

    private void NextLevel() {
        // do something
    }
}