using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    [SerializeField] private int maxScenes = 6;
    private int currentLevel = 2; // Start from level 2

    private void Awake()
    {
        PlayerPrefs.SetInt("CurrentLevel", 2);
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSavedLevel(); // Load the saved level when the game starts
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }


    private void Start()
    {
        LoadSavedLevel(); // Load the saved level when the game starts
    }

    private void LoadSavedLevel()
    {
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 2); // Default to level 2 if not set
        Debug.Log("LoadSavedLevel: " + currentLevel);
    }

    public int GetCurrentLevelIndex()
    {
        return currentLevel;
    }

    public int CompleteLevel()
    {
        currentLevel++;

        if (currentLevel < maxScenes) // If not the last scene
        {
            return currentLevel;
        }
        else // Last scene reached, return menu index
        {
            currentLevel = 2; // Reset to level 2
            return 2; // Menu scene index
        }
    }

    public void QuitToStartScene()
    {
        currentLevel = 2; // Reset to level 2
    }

}
