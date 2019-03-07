using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/**
    DEPRECATED USE SimpleRBAnimatorController instead since most enemies have RBs
    This script simply sets the velocity in any children
    animators to that of the navmeshagent.
 */
[System.Obsolete("If gameobject has RB, use SimpleRBAnimatorController instead.")]
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
