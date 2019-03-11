using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadSceneOnClick : MonoBehaviour
{
    // Spaghetti, I know
    private AudioSource menuMusic;
    [SerializeField] private float delay;


    /* Loads the scene with the given name from the project's build settings (File > Build Settings).
     * If the name is "Quit", it ends the application.
     */ 
    public void Load(string sceneName)
    {
        if (sceneName.Equals("Menu") || sceneName.Equals("Controls"))
        {
            SceneManager.LoadScene(sceneName);
        }
        else if (sceneName.Equals("Quit"))
        {
            Application.Quit();
        }
        else
        {
            menuMusic = MenuMusic.GetMusic();
            StartCoroutine(LoadSceneDelayed(sceneName));
        }
    }


    /* Delays scene transition to allow for menu music to fade.
     */ 
    private IEnumerator LoadSceneDelayed(string sceneName)
    {
        float elapsedTime = 0.0f;
        float currentVolume = menuMusic.volume;

        while(elapsedTime < delay)
        {
            elapsedTime += Time.deltaTime;
            menuMusic.volume = Mathf.Lerp(currentVolume, 0, elapsedTime / delay);
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }
}
