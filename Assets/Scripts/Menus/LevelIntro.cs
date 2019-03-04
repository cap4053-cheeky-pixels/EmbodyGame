using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelIntro : MonoBehaviour
{
    // To be set by other classes as needed before loading the next level
    public static string nameOfSceneToLoad = "Level1";

    // Used for regulating 
    [SerializeField] private float timeToWaitBeforeLoading;
    private float sceneDurationTimer;

    [SerializeField] private Text text;
    private string[] intros = { "WELCOME\nTO\nHELL", "9 CIRCLES\n1 LIFE", "TIRED YET?",
                                "THE END\nIS NIGH", "IT ONLY GETS WORSE",
                                "TIME TO JOIN\nTHE DEAD", "WELCOME TO\nYOUR GRAVE",
                                "A FALLEN ANGEL\nAWAITS", "IT NEVER ENDS", "YOU CANNOT ESCAPE",
                                "RESISTANCE IS FUTILE", "ABANDON THYSELF\nTO FIND THYSELF",
                                "THOSE WHO COME HERE\nNEVER LEAVE", "PERIL LIES AHEAD",
                                "YOU ARE NOT\nTHE FIRST"};

    /* Set up all the text.
     */ 
    private void Start()
    {
        int index = Random.Range(0, intros.Length - 1);
        text.text = intros[index];
        sceneDurationTimer = 0.0f;
    }


    /* Runs each frame.
     */ 
    private void Update()
    {
        sceneDurationTimer += Time.deltaTime;

        if(sceneDurationTimer >= timeToWaitBeforeLoading)
        {
            SceneManager.LoadScene(nameOfSceneToLoad);
        }
    }
}
