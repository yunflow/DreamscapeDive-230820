using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void QuitGame()
    {
        SceneManager.LoadScene("StartMenu"); // Load the start menu
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("Level2"); // Load the next game scene
    }
}
