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

    // A reference to the player game object
    private GameObject player;

    // The distance within which the boss can initiate ranged attacks
    public float rangedDistance;

    // The distance within which the boss can initiate melee attacks
    public float meleeDistance;
    // The pitchfork the devil uses for attacking
    public MeleeWeapon meleeWeapon;
    // Used to delay melee attacks
    private float meleeAttackTimer;

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

        // Set the melee attack timer to the delay for starters
        meleeAttackTimer = meleeWeapon.timeBetweenAttacks;

        // Disable the script until the boss is awoken
        // TODO uncomment this.enabled = false;
    }


    /* Called every frame.
     */ 
    private void Update()
    {
        damageRecoilTimer += Time.deltaTime;
        meleeAttackTimer += Time.deltaTime;
        SelectBehavior();
    }


    /* Returns a reference to this boss's NavMeshAgent.
     */
    public NavMeshAgent GetAgent() { return agent; }


    /* Called when this boss dies. Plays the boss's death animation.
     */ 
    private void OnBossDied(GameObject boss)
    {
        // TODO after music has been added, stop it here
        // TODO maybe add victory audio... or perhaps that goes in RoomScript.cs
        animator.ResetTrigger("Fly");
        animator.ResetTrigger("AttackMelee");
        animator.ResetTrigger("AttackRanged");
        animator.SetTrigger("DeathPose");

        // Prevent this script and the navmesh agent from doing anything else
        this.enabled = false;
        agent.isStopped = true;
    }


    /* Called when the player dies. Plays the boss's victory animation.
     */ 
    private void OnPlayerDied()
    {
        animator.ResetTrigger("Fly");
        animator.ResetTrigger("AttackMelee");
        animator.ResetTrigger("AttackRanged");
        animator.SetTrigger("VictoryPose");

        // Prevent this script from doing anything else
        this.enabled = false;
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

        // TODO stunned
    }


    /* Selects one of three behaviors to perform, depending on the boss's distance
     * from the player. If the boss is within ranged attack range, then it will
     * proceed to attack with projectiles. If the boss is within melee range, it will
     * poke the player with its pitchfork. Otherwise, it will fly towards the player
     * until it is within ranged attack range.
     */ 
    public void SelectBehavior()
    {
        Vector3 vectorToPlayer = player.transform.position - gameObject.transform.position;

        // Flight
        if(vectorToPlayer.magnitude > agent.stoppingDistance)
        {
            animator.ResetTrigger("AttackMelee");
            animator.ResetTrigger("AttackRanged");
            animator.SetTrigger("Fly");
        }
        // Ranged
        else if(vectorToPlayer.magnitude > meleeDistance)
        {
            animator.ResetTrigger("Fly");
            animator.ResetTrigger("AttackMelee");
            animator.SetTrigger("AttackRanged");
        }
        // Melee
        else if (meleeAttackTimer > meleeWeapon.timeBetweenAttacks)
        {
            animator.ResetTrigger("Fly");
            animator.ResetTrigger("AttackRanged");
            animator.SetTrigger("AttackMelee");
            meleeAttackTimer = 0.0f;
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
                    animator.SetTrigger("StopFlying");
                }
            }
        }
    }
}
