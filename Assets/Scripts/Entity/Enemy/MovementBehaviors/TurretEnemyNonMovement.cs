using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemyNonMovement : MonoBehaviour, IOnDeathController
{
    private Transform playerTransform;

    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Vector3 pp = playerTransform.position;
        pp.y = transform.position.y;
        transform.LookAt(pp);
    }

    public void OnDeath()
    {
        enabled = false;
    }
}
