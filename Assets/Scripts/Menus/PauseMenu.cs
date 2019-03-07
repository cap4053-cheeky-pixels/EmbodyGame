using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    // Internal flag to keep track of whether the user has paused
    public static bool gameIsPaused = false;

    // UI canvas for the pause menu
    [SerializeField] private GameObject pauseMenuUI;

    // The event system handling the menu navigation
    [SerializeField] private EventSystem eventSystem;

    // The first button that should be highlighted in the pause menu
    [SerializeField] private Button firstButtonToSelect;


    /* Runs every frame and polls for pausing input.
     */
    private void Update()
    {
        bool playerPaused = Input.GetButtonDown("Pause");
        bool playerCanceled = Input.GetButtonDown("Cancel");

        // Unpausing
        if(gameIsPaused)
        {
            if(playerCanceled)
            {
                Resume();
            }
        }
        // Pausing
        else if(playerPaused)
        {
            Pause();
        }
    }


    /* Unpauses the game and hides the pause menu.
     */ 
    public void Resume()
    {
        eventSystem.SetSelectedGameObject(null);
        pauseMenuUI.SetActive(false);
        gameIsPaused = false;
        Time.timeScale = 1f;
    }


    /* Pauses the game (but not the menu itself).
     */ 
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        firstButtonToSelect.Select();
        gameIsPaused = true;
        Time.timeScale = 0f;
    }


    /* Restarts the entire game.
     */ 
    public void Restart()
    {
        // Necessary because it's static and persists
        gameIsPaused = false;

        Time.timeScale = 1f;
        LevelIntro.nameOfSceneToLoad = "Level1";
        SceneManager.LoadScene("LevelIntro");
    }


    /* Changes the scene to the menu.
     */ 
    public void LoadMenu()
    {
        // Necessary because it's static and persists
        gameIsPaused = false;

        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }


    /* Quits the game.
     */ 
    public void QuitGame()
    {
        Application.Quit();
    }
}
