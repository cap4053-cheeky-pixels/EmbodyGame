using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoorOpen : MonoBehaviour
{
    [SerializeField]
    private GameObject TrapDoor;
    
    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
            //Play animations
            TrapDoor.transform.GetChild(0).GetComponent<Animation>().Play("LeftAnim");
            TrapDoor.transform.GetChild(1).GetComponent<Animation>().Play("HingeRight");
            //Start script that allows player to fall
            TrapDoor.GetComponent<PlayerFall>().trapOpened = true;
        }
        else
        Debug.Log("Entity detected" + other.gameObject.tag);
    }
}
