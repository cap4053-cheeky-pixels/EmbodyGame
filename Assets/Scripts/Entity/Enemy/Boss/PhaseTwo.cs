using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseTwo : MonoBehaviour
{
    // Phase two attack prefab
    [SerializeField] private GameObject phaseTwoAttack;

    // How the environment should change in phase two
    public GameObject phaseTwoProps;

    // Timer used to actually spawn the phase two attack
    private float phaseTwoAttackTimer;

    // Regulates the intervals at which the phase two attack is used
    public float timeToPhaseTwoAttack;


    /* Called when the game starts. Disables the script until it's
     * enabled externally by the owner script.
     */ 
    private void Awake()
    {
        // Set this to zero initially
        phaseTwoAttackTimer = 0.0f;
        
        // Disable the script until it's re-enabled
        this.enabled = false;
    }


    /* Runs every frame while the associated boss is in phase two.
     * Spawns the phase two attack at regular intervals.
     */ 
    private void Update()
    {
        phaseTwoAttackTimer += Time.deltaTime;

        // Meteor attack
        if (phaseTwoAttackTimer > timeToPhaseTwoAttack)
        {
            Instantiate(phaseTwoAttack, gameObject.transform);
            phaseTwoAttackTimer = 0.0f;
        }
    }
}
