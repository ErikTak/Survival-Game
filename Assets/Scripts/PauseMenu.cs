using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject endGameMenuUI;
    public GameObject levelUpMenuUI;
    bool gameHasEnded = false;
    public GameObject[] enemiesToDestroy;

    void Update()
    {
        // Open pausemenu if ESC is pressed and End Game Menu UI is not active
        if (Input.GetKeyDown(KeyCode.Escape) && !endGameMenuUI.activeSelf)
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }    
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void EndGameMenu()
    {
        endGameMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        enemiesToDestroy = GameObject.FindGameObjectsWithTag("Enemy");

        for (var i = 0; i < enemiesToDestroy.Length; i++)
        {
            Destroy(enemiesToDestroy[i]);
        }
    }

    public void Settings()
    {
        Debug.Log("Loading Settings ...");
    }


    // Quit menu that navigates to MainMenu scene or Quits the game

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0); // use (0) or (1) depending on the build settings
    }

    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();
    }

    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("Game Over");
            // Load end screen
            EndGameMenu();
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void ShowLvlUpMenu()
    {
        Time.timeScale = 0;
        levelUpMenuUI.SetActive(true);
    }

    public void HideLvlUpMenu()
    {
        Time.timeScale = 1;
        levelUpMenuUI.SetActive(false);
    }
}
