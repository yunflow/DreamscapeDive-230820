using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void QuitGame()
    {
        AudioManager.Instance.PlaySFX("Click");
        LevelManager.Instance.QuitToStartScene();
        SceneManager.LoadScene("StartMenu"); // Load the start menu
    }

    public void NextLevel()
    {
        AudioManager.Instance.PlaySFX("Click");
        int level = LevelManager.Instance.CompleteLevel();
        SceneManager.LoadScene(level);
    }
}
