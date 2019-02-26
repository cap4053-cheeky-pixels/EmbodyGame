using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;


[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(NavMeshAgent))]
public class DevilBoss : MonoBehaviour
{
    // The NavMeshAgent this boss will use to navigate the room
    private NavMeshAgent agent;

    // Reference to the Enemy script attached to the boss
    private Enemy self;

    // A reference to the player
    private GameObject player;

    // The distance within which the boss can initiate ranged attacks
    public float rangedDistance;

    // The distance within which the boss can initiate melee attacks
    public float meleeDistance;

    // The animator for this boss
    private Animator animator;
    

    /* Initialize all members.
     */ 
    private void Awake()
    {
        self = gameObject.GetComponent<Enemy>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = self.model.GetComponent<Animator>();

        // When moving towards the player, the boss will always stop within its ranged attack radius
        agent.stoppingDistance = rangedDistance - 1;
    }


    /* Returns a reference to this boss's NavMeshAgent.
     */ 
    public NavMeshAgent GetAgent() { return agent; }


    /* Returns a reference to the player this boss is fighting.
     */ 
    public GameObject GetPlayer() { return player; }


    /* Plays the fly animation and moves the boss closer to the player.
     * The boss will stop moving once the player is within its firing range.
     */ 
    public void FlyTowardsPlayer()
    {
        agent.SetDestination(player.transform.position);

        // If we arrive at our destination, go back to Idle
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    animator.SetTrigger("StopFlying");
                }
            }
        }
    }
}
