using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        AudioManager.Instance.PlaySFX("Start");
        SceneManager.LoadScene(2); // Load the game scene
    }
}
