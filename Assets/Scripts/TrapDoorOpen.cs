using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoorOpen : MonoBehaviour
{
    //used to reference particular TrapDoor
    [SerializeField]
    private GameObject TrapDoor;
    
    //send reference of the offending entity, send reference to TrapDoor
    public delegate void TrapOpenedHandler(GameObject fallingEntity);
    public event TrapOpenedHandler TrapOpened;
    
    void OnTriggerEnter(Collider other){
        
        if(other.gameObject.tag == "Player")
        {
            //Play animations
            Animation[] animations = TrapDoor.GetComponentsInChildren<Animation>();
            foreach(Animation ani in animations)
            ani.Play();
            //signal event
            TrapOpened?.Invoke(other.gameObject);
        }
        else
        Debug.Log("Entity detected" + other.gameObject.tag);
    }
    
}
