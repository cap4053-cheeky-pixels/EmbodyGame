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
    private GameObject Player;
    //height from Player we want Camera to be
    private float desiredCameraHeight = 5.0f;
    private Vector3 direction;
    private float timer = 0.0f;
    private float delayuntilFastFall = .5f;
    
    //falling audio at this time overriden in BeginFall method
    [SerializeField]
    private AudioSource fallingAudio;
    
    //flag
    public bool trapOpened {get; set;}
    
    void Start(){
        
        GameObject.Find("TrapDoor").transform.GetChild(0).transform.GetChild(0).GetComponent<TrapDoorOpen>().TrapOpened += BeginFall;
        GameObject.Find("TrapDoor").transform.GetChild(1).transform.GetChild(0).GetComponent<TrapDoorOpen>().TrapOpened += BeginFall;
    }
    
    void BeginFall(){
        direction = new Vector3(0, desiredCameraHeight, 0);
        Player = GameObject.Find("MainPlayer");
        PlayerBod = Player.GetComponent<Rigidbody>();
        fallingAudio = GameObject.FindWithTag("Boss").GetComponents<AudioSource>()[1];
        trapOpened = true;
        ChangeCamera();

    }
    
    void Update(){
        
        if(trapOpened){
            timer += Time.deltaTime;
            Camera.GetComponent<CameraController>().MoveTo(Player.transform.position + direction);
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
    
    void ChangeCamera(){
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled = false;
        Camera = GameObject.Find("ProgressionCamera");
        Camera.GetComponent<Camera>().enabled = true;
    }
}
