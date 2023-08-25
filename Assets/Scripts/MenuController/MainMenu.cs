using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void QuitGame()
    {
        LevelManager.Instance.QuitToStartScene();
        SceneManager.LoadScene("StartMenu"); // Load the start menu
    }

    public void NextLevel()
    {
        int level = LevelManager.Instance.CompleteLevel();
        SceneManager.LoadScene(level);
    }
}
