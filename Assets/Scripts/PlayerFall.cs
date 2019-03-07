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
    
    [SerializeField]
    private AudioSource fallingAudio;
    
    //flag
    private bool trapOpened = false;
    
    void Start(){
        
        Camera = GameObject.FindWithTag("MainCamera");
        TrapDoor = gameObject;
        TrapDoorOpen[] hinges = GetComponentsInChildren<TrapDoorOpen>();
        foreach(TrapDoorOpen script in hinges)
        script.TrapOpened += BeginFall;
        
    }
    
    void BeginFall(GameObject fallingEntity){
        
        direction = new Vector3(0, desiredCameraHeight, 0);
        PlayerBod = fallingEntity.GetComponent<Rigidbody>();
        Player = fallingEntity;
        AudioSource[] audios = GameObject.FindWithTag("Boss").GetComponentsInChildren<AudioSource>();
        foreach(AudioSource asource in audios)
        if(asource.clip.name == "Jes£s Lastra - Cries From Hell")
        fallingAudio = asource;
        trapOpened = true;
        
        //these lines are irrelevant once the boss dies but doesnt hurt to have them
        if(!fallingAudio.isPlaying)
        fallingAudio.Play();
    }
    
    void Update(){
        
        if(trapOpened){
            timer += Time.deltaTime;
            Camera.GetComponent<CameraController>().MoveTo(Player.transform.position + TrapDoor.transform.position + direction);
            fallingAudio.volume += .003f;
            if(timer > delayuntilFastFall)
            //initiate Fast Fall
            Player.transform.position += Vector3.down*.75f;
            if(timer > 5.0f)
            SceneManager.LoadScene("LevelTrans");
        }
    }
}
