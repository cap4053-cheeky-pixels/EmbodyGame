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
    public bool trapOpened {get; set;}
    
    void Start(){
        Camera = GameObject.FindWithTag("MainCamera");
        TrapDoorOpen[] hinges = GetComponentsInChildren<TrapDoorOpen>();
        foreach(TrapDoorOpen script in hinges)
        script.TrapOpened += BeginFall;
    }
    
    void BeginFall(GameObject fallingEntity, GameObject tp){
        direction = new Vector3(0, desiredCameraHeight, 0);
        PlayerBod = fallingEntity.GetComponent<Rigidbody>();
        Player = fallingEntity;
        TrapDoor = tp;
        AudioSource[] audios = GameObject.FindWithTag("Boss").GetComponentsInChildren<AudioSource>();
        foreach(AudioSource asource in audios)
        if(asource.clip.name == "Jes£s Lastra - Cries From Hell")
        fallingAudio = asource;
 
        StartCoroutine("Verga");
    }
    
    void Verga(){
        while(true){
            timer += Time.deltaTime;
            Camera.GetComponent<CameraController>().MoveTo(Player.transform.position + TrapDoor.transform.position + direction);
            fallingAudio.volume += .003f;
            if(timer < delayuntilFastFall){
                PlayerBod.AddForce(Vector3.down*thrust,ForceMode.Acceleration);
            }
            else {
                
            //initiate Fast Fall
            PlayerBod.AddForce(Vector3.down*thrust,ForceMode.Impulse);
            //we must remove the freeze y position constraint and freeze rotation and x,z movement
            PlayerBod.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
            
            }
            if(timer > 5.0f)
            SceneManager.LoadScene("LevelTrans");
        }
    }
}
