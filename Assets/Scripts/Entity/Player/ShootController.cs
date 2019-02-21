using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    // A reference to the player that this script controls
    Player player;


    /* Used to set up this script.
     */ 
    void Awake()
    {
        player = gameObject.GetComponent<Player>();
    }


    /* Listens for key/joystick inputs, changes the player to face the corresponding direction,
     * and instructs the player to fire its weapon in that direction.
     */ 
    void Update()
    {
        if (player.ActionsEnabled())
        {
            float Horizontal = Input.GetAxis("Horizontal");
            float Vertical = Input.GetAxis("Vertical");

            if (Input.GetAxis("FireLeft") != 0)
            {
                player.transform.forward = new Vector3(-1, 0, 0);
                player.FireWeapon();
            }
            else if (Input.GetAxis("FireRight") != 0)
            {
                player.transform.forward = new Vector3(1, 0, 0);
                player.FireWeapon();
            }
            else if (Input.GetAxis("FireUp") != 0)
            {
                player.transform.forward = new Vector3(0, 0, 1);
                player.FireWeapon();
            }
            else if (Input.GetAxis("FireDown") != 0)
            {
                player.transform.forward = new Vector3(0, 0, -1);
                player.FireWeapon();
            }
        }
    }
}
