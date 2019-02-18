using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallWithoutDoor : MonoBehaviour
{
    public float WallWidth = 30;


    /* Called before the game starts. Sets up the wall with the correct dimensions.
     */
    void Awake()
    {
        GameObject wall = gameObject.transform.Find("Cube").gameObject;
        Vector3 wallDimensions = wall.transform.localScale;
        wall.transform.localScale = new Vector3(wallDimensions.x, wallDimensions.y, WallWidth);
    }
}
