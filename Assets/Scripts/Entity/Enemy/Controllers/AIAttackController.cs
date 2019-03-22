using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(ShootController))]
public class AIAttackController : MonoBehaviour, IOnDeathController
{
    private ShootController shootController;
    private FieldOfView fov;


    void Awake()
    {
        shootController = GetComponent<ShootController>();
        fov = GetComponent<FieldOfView>();
    }


    public void OnDeath()
    {
        enabled = false;
    }


    void Update()
    {
        if(fov.PlayerWithinView())
        {
            shootController.FireWeapon();
        }
    }
}
