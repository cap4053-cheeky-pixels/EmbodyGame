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
    [HideInInspector] public NavMeshAgent agent;
    private Vector3 cachedDirection;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Method should only be called once per frame
    public void Move(Vector3 direction)
    {
        agent.SetDestination(transform.position + direction);
    }

    public void MoveTo(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

    public void Stop()
    {
        if(agent.enabled)
        {
            agent.isStopped = true;
            agent.enabled = false;
        }
    }

    // Avoid calling this if possible
    public void Disable()
    {
        agent.enabled = false;
    }
}
