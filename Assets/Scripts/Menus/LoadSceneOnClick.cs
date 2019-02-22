using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    /* Loads the scene with the given index in the project's build settings (File > Build Settings).
     * If sceneIndex is -1, it quits the application.
     */ 
    public void Load(int sceneIndex)
    {
        if(sceneIndex != -1)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Application.Quit();
        }
    }
}
