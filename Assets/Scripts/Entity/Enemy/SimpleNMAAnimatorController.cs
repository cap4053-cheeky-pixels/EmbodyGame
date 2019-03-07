using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/**
    This script simply sets the velocity in any children
    animators to that of the navmeshagent.
 */
[RequireComponent(typeof(NavMeshAgent))]
public class SimpleNMAAnimatorController : MonoBehaviour, IOnDeathController
{
    Animator ani;
    NavMeshAgent nma;

    void Awake()
    {
        nma = GetComponent<NavMeshAgent>();
        ani = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (ani == null || nma == null) return;

        ani.SetFloat("velocity", nma.velocity.magnitude);
    }

    public void OnDeath()
    {
        ani?.SetTrigger("dead");
    }
}
