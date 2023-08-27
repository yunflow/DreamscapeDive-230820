using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishingLine : MonoBehaviour {

    public Score score;
    [SerializeField] private int goal = 2;
    public int Goal => goal;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<PlayerMovement>()) {
            DetectingStar();
        }
    }

    private void DetectingStar() {
        if (score.score >= goal)
        {
            TotleTimeKeeper.Instance.PauseTotalTime();
            AudioManager.Instance.musicSource.Stop();
            AudioManager.Instance.PlaySFX("Win");
            Debug.Log("WIN!!!");
            NextLevel();
        }
            
    }

    private void NextLevel() {
        SceneManager.LoadScene("MainMenu");
        AudioManager.Instance.musicSource.Play();
    }
}