using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/**
    This script simply sets the valocity in any children
    animators to that of the navmeshagent.
 */
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Enemy))]
public class SimpleNMAAnimatorController : MonoBehaviour
{
    Animator ani;
    NavMeshAgent nma;
    Enemy e;

    void Awake()
    {
        nma = GetComponent<NavMeshAgent>();
        ani = GetComponentInChildren<Animator>();
        e = GetComponent<Enemy>();
        e.deathEvent += enemyDead;
    }

    void Update()
    {
        if (ani == null || nma == null) return;

        ani.SetFloat("velocity", nma.velocity.magnitude);
    }

    void enemyDead(GameObject enemy)
    {
        ani?.SetTrigger("dead");
    }
}
