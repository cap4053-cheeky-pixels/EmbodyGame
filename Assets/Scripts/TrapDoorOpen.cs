using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoorOpen : MonoBehaviour
{
    public GameObject TrapDoor;
    
    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
        TrapDoor.transform.GetChild(0).GetComponent<Animation>().Play("LeftAnim");
        TrapDoor.transform.GetChild(1).GetComponent<Animation>().Play("HingeRight");
        }
        else
        Debug.Log("Entity detected" +other.gameObject.tag);
    }
}
