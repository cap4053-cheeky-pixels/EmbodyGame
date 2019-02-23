using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
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

    public void FireWeaponTowards(Vector3 direction)
    {
        // Turn the player
        player.transform.forward = direction;
        // TODO Extract FireWeapon logic in this object
        player.FireWeapon();
    }
}
