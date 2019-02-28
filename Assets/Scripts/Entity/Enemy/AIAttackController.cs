using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShootController))]
public class AIAttackController : MonoBehaviour, IOnDeathController
{
    ShootController sc;
    GameObject playerGO;

    public void OnDeath()
    {
        enabled = false;
    }

    void Awake()
    {
        sc = GetComponent<ShootController>(); // Should be an AttackController interface
        playerGO = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Vector3 direction = playerGO.transform.position - transform.position;
        direction.y = 0;
        sc.FireWeaponTowards(direction.normalized);
    }
}
