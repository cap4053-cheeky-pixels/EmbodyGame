using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(ShootController))]
public class InputController : MonoBehaviour
{
    private Player player;
    private PlayerMovement playerMovement;
    private ShootController shootController;

    // Start is called before the first frame update
    void Awake()
    {
        player = GetComponent<Player>();
        playerMovement = GetComponent<PlayerMovement>();
        shootController = GetComponent<ShootController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.ActionsEnabled())
        {
            HandleMovement();
            HandleFiring();
        }
    }

    void HandleMovement()
    {
        float hori = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        if (hori != 0 || vert != 0)
        {
            playerMovement.Move(new Vector3(hori, 0, vert));
        }
    }

    void HandleFiring()
    {
        // TODO if player actions are disabled, return
        float fireHori = Input.GetAxis("FireHorizontal");
        float fireVert = Input.GetAxis("FireVertical");

        if (fireHori != 0 || fireVert != 0)
        {
            if (Mathf.Abs(fireHori) > Mathf.Abs(fireVert))
            {
                // Firing horizontally
                shootController.FireWeaponTowards(new Vector3(fireHori, 0, 0).normalized);
            }
            else
            {
                // Firing vertically
                shootController.FireWeaponTowards(new Vector3(0, 0, fireVert).normalized);
            }
        }
    }
}
