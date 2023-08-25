using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        AudioManager.Instance.PlaySFX("Start");
        SceneManager.LoadScene("MainLevel"); // Load the game scene
    }
}
