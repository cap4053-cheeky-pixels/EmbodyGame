using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
    Basic enemy movement that moves towards the player.
 */
[RequireComponent(typeof(EnemyMovement))]
public class BasicEnemyMovement : MonoBehaviour
{
    private EnemyMovement em;
    private Transform playerTransform;

    void Awake()
    {
        em = GetComponent<EnemyMovement>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        em.MoveTo(playerTransform.position);
    }
}
