using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DoorController : MonoBehaviour
{
    private Animator animator;

    private bool doorOpen;
    public bool IsOpen() { return doorOpen; }


    /* Called on scene enter */
    void Start()
    {
        doorOpen = false;
        animator = GetComponent<Animator>();
        animator.speed = 4f;
    }


    /* Opens the Door object this script is associated with */
    public void Open()
    {
        if (!doorOpen)
        {
            doorOpen = true;
            ChangeDoorState("Open");
        }
    }


    /* Closes the Door object this script is associated with */
    public void Close()
    {
        if (doorOpen)
        {
            doorOpen = false;
            ChangeDoorState("Close");
        }
    }


    /* Changes the associated Door object's state via its animator component */
    void ChangeDoorState(string state)
    {
        animator.SetTrigger(state);
    }
}