using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallWithDoor : MonoBehaviour
{
    public float DoorWidth = 4;
    public float WallWidth = 30;


    /* Called before the game starts. Sets up the wall with the correct dimensions.
     */ 
    void Awake()
    {
        // References for convenience
        GameObject left = gameObject.transform.Find("Left").gameObject;
        GameObject right = gameObject.transform.Find("Right").gameObject;
        GameObject top = gameObject.transform.Find("Top").gameObject;
        GameObject door = gameObject.transform.Find("Door").gameObject;

        // Scale the door
        Vector3 doorDimensions = door.transform.localScale;
        door.transform.localScale = new Vector3(doorDimensions.x, doorDimensions.y, DoorWidth);

        // Scale the top part of the wall
        Vector3 topDimensions = top.transform.localScale;
        top.transform.localScale = new Vector3(topDimensions.x, topDimensions.y, WallWidth);

        // Scale the left part of the wall
        Vector3 leftDimensions = left.transform.localScale;
        left.transform.localScale = new Vector3(leftDimensions.x, leftDimensions.y, (WallWidth - DoorWidth) / 2);

        // Scale the right part of the wall
        Vector3 rightDimensions = right.transform.localScale;
        right.transform.localScale = new Vector3(rightDimensions.x, rightDimensions.y, (WallWidth - DoorWidth) / 2);

        // Position the left and right parts of the wall
        float offsetFromDoor = ((left.transform.localScale.z / 2) + (DoorWidth / 2));

        Vector3 leftPos = left.transform.position;
        left.transform.localPosition = new Vector3(0.25f, 2, door.transform.localPosition.z - offsetFromDoor);

        Vector3 rightPos = right.transform.position;
        right.transform.localPosition = new Vector3(0.25f, 2, door.transform.localPosition.z + offsetFromDoor);
    }
}
