using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    // public static GameManager Instance; // Singleton instance of the GameManager

    public int startingLevelIndex = 0; // Index of the starting level
    public float levelLoadDelay = 1f; // Delay before loading the next level

    private static int currentLevelIndex = 0; // Index of the current level

    private static bool isSwitchingLevel = false;

    private KeyCode[] keyCodes = {
        KeyCode.Alpha0,
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6,
        KeyCode.Alpha7,
        KeyCode.Alpha8,
        KeyCode.Alpha9
    };

    // private void Awake()
    // {
    //     // Ensure only one instance of the GameManager exists
    //     if (Instance == null)
    //     {
    //         Instance = this;
    //         DontDestroyOnLoad(gameObject);
    //     }
    //     else
    //     {
    //         Destroy(gameObject);
    //     }
    // }

    private void Start()
    {
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        // Reload Scene
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(currentLevelIndex);
        }

        if (Input.GetKeyDown(KeyCode.Minus))
        {
            LoadPreviousLevel();
        }

        if (Input.GetKeyDown(KeyCode.Plus))
        {
            LoadNextLevel();
        }

    }

    public void LoadNextLevel()
    {
        currentLevelIndex++;
        LoadLevel(currentLevelIndex);
    }
    public void LoadPreviousLevel()
    {
        currentLevelIndex--;
        LoadLevel(currentLevelIndex);
    }


    private void LoadLevel(int levelIndex)
    {
        if (!isSwitchingLevel)
        {
            // Check if the level index is within the valid range
            if (levelIndex >= 0 && levelIndex < SceneManager.sceneCountInBuildSettings)
            {
                StartCoroutine(LoadLevelWithDelay(levelIndex));
            }
            else
            {
                Debug.LogWarning("Invalid level index: " + levelIndex);
            }

        }
    }

    private IEnumerator LoadLevelWithDelay(int levelIndex)
    {
        isSwitchingLevel = true;
        yield return new WaitForSeconds(levelLoadDelay);
        SceneManager.LoadScene(levelIndex);
        isSwitchingLevel = false;
    }
}

