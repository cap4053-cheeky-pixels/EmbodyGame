using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShootController))]
public class AIAttackController : MonoBehaviour, IOnDeathController
{
    ShootController sc;

    public void OnDeath()
    {
        enabled = false;
    }

    void Awake()
    {
        enabled = false;
        sc = GetComponent<ShootController>(); // Should be an AttackController interface
    }

    void Update()
    {
        sc.FireWeapon();
    }
}
