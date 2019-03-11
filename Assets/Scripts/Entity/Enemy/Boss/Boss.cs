using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(NavMeshAgent))]
public abstract class Boss : MonoBehaviour
{
    // The NavMeshAgent this boss will use to navigate the room
    protected NavMeshAgent agent;

    // Reference to the Enemy script attached to the boss
    protected Enemy self;

    // A reference to the player game object
    protected GameObject player;

    // The animator for this boss
    protected Animator animator;

    // The boss's fighting audio theme
    [SerializeField] protected AudioSource audioTheme;

    // The audio that should play when the boss is slain
    [SerializeField] protected AudioSource deathAudio;

    // Used to signal the start of the boss battle to any relevant listeners
    public delegate void BossBattleStarted();
    public static event BossBattleStarted bossBattleStarted;


    /* Sets up the boss with all of its standard/required fields.
     */ 
    protected virtual void Awake()
    {
        self = gameObject.GetComponent<Enemy>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = self.model.GetComponent<Animator>();
    }


    /* Raises the bossBattleStarted event. Required for raising
     * inherited events, as they can't be raised directly in a subclass.
     */
    protected virtual void SignalStartOfBossBattle()
    {
        bossBattleStarted?.Invoke();
    }
}
