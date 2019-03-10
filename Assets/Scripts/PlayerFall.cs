using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerFall : MonoBehaviour
{
    private Rigidbody PlayerBod;
    //speed at which the player is dragged down
    public float thrust = 10.0f;
    private GameObject Camera;
    private GameObject TrapDoor;
    private GameObject Player;
    //height from Player we want Camera to be
    private float desiredCameraHeight = 5.0f;
    private Vector3 direction;
    private float timer = 0.0f;
    private float delayuntilFastFall = .5f;
    private float rateofFall = 30.0f;
    private float timetoNextScene = 5.0f;
    
    [SerializeField]
    private AudioSource fallingAudio;
    
    //flag
    private bool trapOpened = false;
    
    void Start()
    {
        direction = new Vector3(0, desiredCameraHeight, 0);
        Camera = GameObject.FindWithTag("MainCamera");
        TrapDoor = gameObject;
        TrapDoorOpen[] hinges = GetComponentsInChildren<TrapDoorOpen>();
        foreach(TrapDoorOpen script in hinges)
        {
            script.TrapOpened += BeginFall;
        }
        
    }
    
    void BeginFall(GameObject fallingEntity)
    {
        PlayerBod = fallingEntity.GetComponent<Rigidbody>();
        Player = fallingEntity;
        AudioSource[] audios = GameObject.FindWithTag("Boss").GetComponentsInChildren<AudioSource>();
        foreach(AudioSource asource in audios)
        {
            if(asource.clip.name == "Jes£s Lastra - Cries From Hell")
            {
                fallingAudio = asource;
            }
        }
        trapOpened = true;
        Player.GetComponent<InputController>().enabled = false;
        Camera.GetComponent<CameraController>().MoveTo(Player.transform.position + direction);
    }
    
    void Update()
    {
        if(trapOpened)
        {
            timer += Time.deltaTime;
            fallingAudio.volume += .003f;
            if(timer > delayuntilFastFall)
            {
                //initiate Fast Fall
                Player.transform.position += Vector3.down*rateofFall*Time.deltaTime;
            }
            if(timer > timetoNextScene)
            {
                SceneManager.LoadScene("LevelTrans");
            }
        }
    }
}
