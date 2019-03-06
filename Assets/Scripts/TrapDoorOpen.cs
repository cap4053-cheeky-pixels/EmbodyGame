using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoorOpen : MonoBehaviour
{
    [SerializeField]
    private GameObject TrapDoor;
    
    public delegate void TrapOpenedHandler();
    public event TrapOpenedHandler TrapOpened;
    
    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
            //Play animations
            TrapDoor.transform.GetChild(0).GetComponent<Animation>().Play("LeftAnim");
            TrapDoor.transform.GetChild(1).GetComponent<Animation>().Play("HingeRight");
            
            //signal event
            TrapOpened?.Invoke();
        }
        else
        Debug.Log("Entity detected" + other.gameObject.tag);
    }
    
}
