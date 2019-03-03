using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PhaseTwo))]
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
    // The ranged weapon the devil uses to fire projectiles
    [SerializeField] private ProjectileWeapon rangedWeapon;
    // Used to delay ranged attacks
    private float rangedAttackTimer;

    // The distance within which the boss can initiate melee attacks
    public float meleeDistance;
    // The pitchfork the devil uses for attacking
    [SerializeField] private MeleeWeapon meleeWeapon;
    // Used to delay melee attacks
    private float meleeAttackTimer;

    // The animator for this boss
    private Animator animator;

    // Phase two stuff
    private bool inPhaseTwo;
    [SerializeField] AudioSource phaseTwoAudio;
    private PhaseTwo phaseTwo;

    // Used to display the boss's health in game
    public delegate void BossBattleStarted();
    public static event BossBattleStarted bossBattleStarted;


    /* Initialize all members.
     */
    private void Awake()
    {
        bossBattleStarted?.Invoke();

        // All standard components
        self = gameObject.GetComponent<Enemy>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = self.model.GetComponent<Animator>();
        phaseTwo = gameObject.GetComponent<PhaseTwo>();

        // When moving towards the player, the boss will always stop within its ranged attack radius
        agent.stoppingDistance = rangedDistance - 1; 

        // Boss subscribes to its own health change events
        self.healthChangedEvent += OnBossHealthChanged;

        // Boss also subscribes to its own death event
        self.deathEvent += OnBossDied;

        // And finally, boss subscribes to the player's death (for victory animation)
        player.GetComponent<Player>().deathEvent += OnPlayerDied;
        
        // Set the melee attack timer to the delay initially
        meleeAttackTimer = meleeWeapon.timeBetweenAttacks;
        
        // Set the ranged attack timer to the delay initially
        rangedAttackTimer = rangedWeapon.timeBetweenAttacks;
    }


    /* Called every frame.
     */ 
    private void Update()
    {
        meleeAttackTimer += Time.deltaTime;
        rangedAttackTimer += Time.deltaTime;

        RotateToFacePlayer();
        SelectBehavior();
    }


    /* Called when this boss dies. Plays the boss's death animation.
     */ 
    private void OnBossDied(GameObject boss)
    {
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
        // Initiate phase 2
        if(self.Health < self.MaxHealth / 2 && !phaseTwo.enabled)
        {
            animator.SetTrigger("BecomeStunned");
            phaseTwoAudio.Play();
            phaseTwo.enabled = true;

            if(phaseTwo.phaseTwoProps != null)
            {
                Instantiate(phaseTwo.phaseTwoProps, gameObject.transform.parent);
            }
        }
    }


    /* Selects one of three behaviors to perform, depending on the boss's distance
     * from the player. If the boss is within ranged attack range, then it will
     * proceed to attack with projectiles. If the boss is within melee range, it will
     * poke the player with its pitchfork. Otherwise, it will fly towards the player
     * until it is within ranged attack range.
     */ 
    public void SelectBehavior()
    {
        float distanceToPlayer = (player.transform.position - gameObject.transform.position).magnitude;

        // Flight
        if(distanceToPlayer > agent.stoppingDistance)
        {
            animator.ResetTrigger("AttackMelee");
            animator.ResetTrigger("AttackRanged");
            animator.SetTrigger("Fly");
        }
        // Ranged
        else if(distanceToPlayer > meleeDistance &&
                rangedAttackTimer > rangedWeapon.timeBetweenAttacks)
        {
            animator.ResetTrigger("Fly");
            animator.ResetTrigger("AttackMelee");
            animator.SetTrigger("AttackRanged");
            rangedAttackTimer = 0.0f;
        }
        // Melee
        else if (distanceToPlayer <= meleeDistance &&
                 meleeAttackTimer > meleeWeapon.timeBetweenAttacks)
        {
            animator.ResetTrigger("Fly");
            animator.ResetTrigger("AttackRanged");
            animator.SetTrigger("AttackMelee");
            meleeAttackTimer = 0.0f;
        }
    }


    /* It's in the name.
     */ 
    private void RotateToFacePlayer()
    {
        Quaternion newRotation = Quaternion.LookRotation(player.transform.position - gameObject.transform.position);
        gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, newRotation, 5);
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
                    animator.SetTrigger("Idle");
                }
            }
        }
    }
}
