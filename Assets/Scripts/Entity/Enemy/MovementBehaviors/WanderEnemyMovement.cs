using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class WanderEnemyMovement : MonoBehaviour
{
    public float wanderCircleRadius;
    public float wanderStr;
    private Vector3 currentDirection;
    private Vector3 wanderCirclePoint;
    private EnemyMovement em;

    void Awake()
    {
        em = GetComponent<EnemyMovement>();
        wanderCirclePoint = new Vector3(0, 0, 0);
    }

    void Start()
    {
        currentDirection = transform.forward;
    }

    void Update()
    {
        Wander();
        em.Move(currentDirection);
    }

    void Wander()
    {
        // This is a very shit wander
        wanderCirclePoint.x = Mathf.Clamp(Mathf.Cos(wanderCirclePoint.x + Random.Range(-wanderStr, wanderStr)), -1, 1);
        wanderCirclePoint.z = Mathf.Clamp(Mathf.Cos(wanderCirclePoint.z + Random.Range(-wanderStr, wanderStr)), -1, 1);

        Vector3 wanderPoint = (wanderCircleRadius * wanderCirclePoint) + (transform.forward * wanderCircleRadius);
        currentDirection = wanderPoint.normalized;
    }
}
