using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/**
    This class should be on every enemy that can move. No path finding
    or movement calculation should be done here, simple exposes a simple
    move method for scripts that do path finding.
*/
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent nma;
    private Vector3 cachedDirection;

    void Awake()
    {
        nma = GetComponent<NavMeshAgent>();
    }

    // Method should only be called once per frame
    public void Move(Vector3 direction)
    {
        nma.SetDestination(transform.position + direction);
    }

    public void MoveTo(Vector3 destination)
    {
        nma.SetDestination(destination);
    }
}
