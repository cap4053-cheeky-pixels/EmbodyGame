using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class WanderEnemyMovement : MonoBehaviour
{
    private Vector3 currentDirection;
    private EnemyMovement em;

    void Awake()
    {
        em = GetComponent<EnemyMovement>();
    }

    void Start()
    {
        currentDirection = transform.forward;
    }

    void Update()
    {
        Wander();
        em.Move(currentDirection);
        transform.forward = currentDirection; // ew, get this shit outa here
    }

    void Wander()
    {
        // This is a very shit wander
        currentDirection = Quaternion.Euler(0, Random.Range(-5, 5), 0) * currentDirection;
    }
}
