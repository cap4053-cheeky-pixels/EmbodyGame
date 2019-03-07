using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelIntro : MonoBehaviour
{
    // To be set by other classes as needed before loading the next level
    public static string nameOfSceneToLoad = "Level1";

    // Used for regulating 
    [SerializeField] private float timeToWaitBeforeSceneChange;

    [SerializeField] private Text text;
    private string[] intros = { "WELCOME\nTO\nHELL", "9 CIRCLES\n1 LIFE", "TIRED YET?",
                                "THE END\nIS NIGH", "IT ONLY GETS WORSE",
                                "TIME TO JOIN\nTHE DEAD", "WELCOME TO\nYOUR GRAVE",
                                "A FALLEN ANGEL\nAWAITS", "IT NEVER ENDS", "YOU CANNOT ESCAPE",
                                "RESISTANCE IS FUTILE", "ABANDON THYSELF\nTO FIND THYSELF",
                                "THOSE WHO COME HERE\nNEVER LEAVE", "PERIL LIES AHEAD",
                                "YOU ARE NOT\nTHE FIRST"};


    /* Set up all the text and start the timer.
     */ 
    private void Start()
    {
        int index = Random.Range(0, intros.Length - 1);
        text.text = intros[index];
        StartCoroutine(SceneTimer());
    }


    /* Waits the given number of seconds before emitting the signal to change scenes.
     */ 
    private IEnumerator SceneTimer()
    {
        yield return new WaitForSeconds(timeToWaitBeforeSceneChange);
        ChangeScene();
    }


    /* Called when it's time to change the scene. Loads the scene specified
     * by the string field.
     */ 
    private void ChangeScene()
    {
        SceneManager.LoadScene(nameOfSceneToLoad);
    }
}
