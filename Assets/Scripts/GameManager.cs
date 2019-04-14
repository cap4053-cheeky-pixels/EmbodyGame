using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    // Internal flag to keep track of whether the user has paused
    public bool gameIsPaused = false;

    // Reference to the player
    [SerializeField] private Player player;

    // UI canvas for the pause menu
    [SerializeField] private GameObject pauseMenuUI;

    // The event system handling the menu navigation
    [SerializeField] private EventSystem eventSystem;

    // The first button that should be highlighted in the pause menu
    [SerializeField] private Button firstButtonForPause;

    // The first button that should be highlighted when the game is over
    [SerializeField] private Button firstButtonForGameOver;

    // Reference to the game over text, invisible by default
    [SerializeField] private GameObject gameOverCanvas;

    // The audio to play when the game is over
    [SerializeField] private AudioSource gameOverAudio;

    // The audio to play during gameplay
    [SerializeField] private AudioSource backgroundAudio;

    // Used to hard stop the background audio (Boss battle)
    public void StopBackgroundMusic()
    {
        backgroundAudio.Stop();
    }

    /* Set up any subscription/initialization.
     */ 
    private void Awake()
    {
        player.deathEvent += GameOver;
        backgroundAudio.Play();
    }

    /* Runs every frame and polls for pausing input.
     */
    private void Update()
    {
        bool playerPaused = Input.GetButtonDown("Pause");

        if(!gameIsPaused && playerPaused)
        {
            Pause();
        }
    }


    /* Unpauses the game and hides the pause menu.
     */ 
    public void Resume()
    {
        CursorManager.HideCursor();
        eventSystem.SetSelectedGameObject(null);
        pauseMenuUI.SetActive(false);
        gameIsPaused = false;
        Time.timeScale = 1f;
    }


    /* Pauses the game (but not the menu itself).
     */ 
    public void Pause()
    {
        CursorManager.ShowCursor();
        pauseMenuUI.SetActive(true);

        if(firstButtonForGameOver.enabled)
        {
            firstButtonForPause.Select();
        }

        gameIsPaused = true;
        Time.timeScale = 0f;
    }


    /* Restarts the entire game.
     */ 
    public void Restart()
    {
        backgroundAudio.Stop();
        CursorManager.HideCursor();
        Time.timeScale = 1f;
        LevelIntro.nameOfSceneToLoad = "Level1";
        SceneManager.LoadScene("LevelIntro");
    }


    /* Changes the scene to the menu.
     */ 
    public void LoadMenu()
    {
        CursorManager.ShowCursor();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }


    /* Game-over transition.
     */ 
    public void GameOver()
    {
        Pause();
        firstButtonForPause.interactable = false;
        firstButtonForGameOver.Select();
        gameOverCanvas.SetActive(true);
        backgroundAudio.Stop();
        gameOverAudio.Play();
    }


    /* Quits the game.
     */ 
    public void QuitGame()
    {
        Application.Quit();
    }
}
