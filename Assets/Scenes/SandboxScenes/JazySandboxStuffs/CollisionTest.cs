using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    private float timer = 0;
    private float delay = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    void OnTriggerEnter(Collider c)
    {
        if (timer < delay) return;
        Debug.Log(tag + " Trigger Enter: " + c.tag);
    }

    void OnTriggerStay(Collider c)
    {
        if (timer < delay) return;
        // Warning, spams the console
        //Debug.Log(tag + " Trigger Stay: " + c.tag);
    }

    void OnTriggerExit(Collider c)
    {
        if (timer < delay) return;
        Debug.Log(tag + " Trigger Exit: " + c.tag);
    }

    void OnCollisionEnter(Collision c)
    {
        if (timer < delay) return;
        Debug.Log(tag + " Collision Enter: " + c.collider.tag);
    }

    void OnCollisionStay(Collision c)
    {
        if (timer < delay) return;
        // Warning, spams the console
        //Debug.Log(tag + " Collision Stay: " + c.collider.tag);
    }

    void OnCollisionExit(Collision c)
    {
        if (timer < delay) return;
        Debug.Log(tag + " Collision Exit: " + c.collider.tag);
    }

    // Unique to CharacterController, called like OnCollisionStay
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (timer < delay) return;
        // Constantly called when touching the floor
        if (hit.collider.tag == "Finish" || timer < 5) return;
        timer = 0;
        Debug.Log(tag + " CC Hit: " + hit.collider.tag);
    }

}
