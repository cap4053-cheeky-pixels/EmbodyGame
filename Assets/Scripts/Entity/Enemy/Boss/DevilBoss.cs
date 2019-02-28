using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


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

    // Used to control the delay between damage recoil animations
    [SerializeField]
    private float delayBetweenDamageRecoils;
    private float damageRecoilTimer;
    

    /* Initialize all members.
     */ 
    private void Awake()
    {
        self = gameObject.GetComponent<Enemy>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = self.model.GetComponent<Animator>();

        // When moving towards the player, the boss will always stop within its ranged attack radius
        agent.stoppingDistance = rangedDistance - 1; // TODO magic number, I know...

        // Boss subscribes to its own health change events
        self.healthChangedEvent += OnBossHealthChanged;

        // Boss also subscribes to its own death event
        self.deathEvent += OnBossDied;

        // And finally, boss subscribes to the player's death (for victory animation)
        player.GetComponent<Player>().deathEvent += OnPlayerDied;

        // Set the damage recoil timer to the delay for starters
        damageRecoilTimer = delayBetweenDamageRecoils;
    }


    /* Called every frame.
     */ 
    private void Update()
    {
        damageRecoilTimer += Time.deltaTime;
    }


    /* Returns a reference to this boss's NavMeshAgent.
     */
    public NavMeshAgent GetAgent() { return agent; }


    /* Returns a reference to the player this boss is fighting.
     */ 
    public GameObject GetPlayer() { return player; }


    /* Called when this boss dies. Plays the boss's death animation.
     */ 
    private void OnBossDied(GameObject boss)
    {
        // TODO after music has been added, stop it here
        // TODO maybe add victory audio... or perhaps that goes in RoomScript.cs
        animator.SetTrigger("DeathPose");
    }


    /* Called when the player dies. Plays the boss's victory animation.
     */ 
    private void OnPlayerDied()
    {
        animator.SetTrigger("VictoryPose");
    }


    /* Called when the boss's health changes (but doesn't hit zero).
     * Used to control various logic, such as getting stunned, playing
     * the death animation, or transition to "phase 2".
     */
    private void OnBossHealthChanged()
    {
        // Play the RecoilFromDamage animation every delayBetweenDamageRecoils seconds
        if(damageRecoilTimer > delayBetweenDamageRecoils)
        {
            animator.SetTrigger("RecoilFromDamage");
            damageRecoilTimer = 0.0f;
        }       

        // If half health remaining, play stunned animation
        // TODO but this will play only if it's exactly half... what if overshoots?
        if(self.Health == self.MaxHealth / 2)
        {

        }
    }


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
                    // TODO a little buggy because it sometimes immediately goes to idle...
                    animator.SetTrigger("StopFlying");
                }
            }
        }
    }



}
