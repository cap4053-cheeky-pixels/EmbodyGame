using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class DevilBoss : MonoBehaviour
{
    // The NavMeshAgent this boss will use to navigate the room
    private NavMeshAgent agent;

    // A reference to the player
    private GameObject player;

    // The distance within which the boss can initiate ranged attacks
    public float rangedDistance;

    // The distance within which the boss can initiate melee attacks
    public float meleeDistance;


    /* Initialize all members.
     */ 
    private void Awake()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");

        // When moving towards the player, the boss will always stop within its ranged attack radius
        agent.stoppingDistance = rangedDistance - 1;
    }


    /* Returns a reference to this boss's NavMeshAgent.
     */ 
    public NavMeshAgent GetAgent() { return agent; }


    /* Returns a reference to the player this boss is fighting.
     */ 
    public GameObject GetPlayer() { return player; }
}
