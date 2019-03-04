using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

 
public class MenuMusic : MonoBehaviour
{
    // Singleton design pattern to avoid audio duplication
    private static MenuMusic instance = null;
    public static MenuMusic Instance { get { return instance; } }


    /* Set up the menu music manager.
     */  
    void Awake()
    {
        // Singleton
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        // Subscribe to the activeSceneChanged event
        SceneManager.activeSceneChanged += NewSceneLoaded;

        // Prevent self-destruction upon scene change, handle destruction manually
        DontDestroyOnLoad(instance.gameObject);
    }


    /* Called when a new scene is loaded. Ensures that the menu music stops only if
     * the new scene is not the title screen (menu) or controls.
     */ 
    void NewSceneLoaded(Scene current, Scene next)
    {
        // That is, the music will span the menu and controls scenes
        if(next.name != "Menu" && next.name != "Controls")
        {
            Destroy(gameObject);
        }
    }
}
