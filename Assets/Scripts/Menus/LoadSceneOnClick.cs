using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    /* Loads the scene with the given name from the project's build settings (File > Build Settings).
     * If the name is "Quit", it ends the application.
     */ 
    public void Load(string sceneName)
    {
        if(sceneName == "Quit")
        {
            Application.Quit();
        }

        SceneManager.LoadScene(sceneName);
    }
}
