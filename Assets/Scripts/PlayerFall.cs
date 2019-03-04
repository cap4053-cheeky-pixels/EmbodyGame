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
    private float delayuntilFastFall = 1.0f;
    //falling audio
    [SerializeField]
    private AudioSource fallingAudio;
    //flags
    private bool AudioPlayed = false;
    public bool trapOpened {get; set;}
    private bool CameraChanged = false;
    
    void Awake(){
        direction = new Vector3(0, desiredCameraHeight, 0);
        Player = GameObject.Find("MainPlayer");
        PlayerBod = Player.GetComponent<Rigidbody>();
        fallingAudio = GetComponent<AudioSource>();
        //we must remove the freeze y position constraint
        PlayerBod.constraints = RigidbodyConstraints.FreezeRotation;
        
    }
    
    void Update(){
        
        if(trapOpened){
            if(!CameraChanged)
            ChangeCamera();
            timer += Time.deltaTime;
            Camera.GetComponent<CameraController>().MoveTo(Player.transform.position + direction);
            fallingAudio.volume += .003f;
            AudioPlayed = true;
            if(timer < delayuntilFastFall){
                PlayerBod.AddForce(Vector3.down*thrust,ForceMode.Acceleration);
            }
            else
            //initiate Fast Fall
            PlayerBod.AddForce(Vector3.down*thrust,ForceMode.Impulse);
            if(timer > 5.0f)
            SceneManager.LoadScene("LevelTrans");
        }
        
    }
    
    void ChangeCamera(){
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled = false;
        Camera = GameObject.Find("ProgressionCamera");
        Camera.GetComponent<Camera>().enabled = true;
        CameraChanged = true;
    }
}
