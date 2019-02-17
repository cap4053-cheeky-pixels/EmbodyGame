using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider c)
    {
        Debug.Log("Trigger Enter: " + c.tag);
    }

    void OnTriggerStay(Collider c)
    {
        // Warning, spams the console
        //Debug.Log("Trigger Stay: " + c.gameObject.tag);
    }

    void OnTriggerExit(Collider c)
    {
        Debug.Log("Trigger Exit: " + c.gameObject.tag);
    }

    void OnCollisionEnter(Collision c)
    {
        Debug.Log("Collision Enter: " + c.collider.tag);
    }

    void OnCollisionStay(Collision c)
    {
        // Warning, spams the console
        //Debug.Log("Collision Stay: " + c.collider.gameObject.tag);
    }

    void OnCollisionExit(Collision c)
    {
        Debug.Log("Collision Exit: " + c.collider.gameObject.tag);
    }

}
