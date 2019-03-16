using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DetectPlayer : MonoBehaviour
{
    // Parent object of this controller
    private GameObject parentObject;

    // Reference to the attack controller script dictating when the AI attacks
    private AIAttackController attackController;

    // Reference to the box collider used to detect the player
    private BoxCollider boxCollider;

    


    /* Set up all field references and event subscription.
     */ 
    private void Awake()
    {
        parentObject = gameObject.transform.parent.gameObject;

        Enemy enemy = parentObject.GetComponent<Enemy>();
        enemy.deathEvent += OnEnemyDied;

        attackController = parentObject.GetComponent<AIAttackController>();
        boxCollider = gameObject.GetComponent<BoxCollider>();
    }


    /* When the player is detected, enable the attack controller.
     */
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        Vector3 direction = other.gameObject.transform.position - parentObject.transform.position;

        RaycastHit hit;

        if (Physics.Raycast(parentObject.transform.position, direction, out hit))
        {
            if (hit.transform.gameObject.CompareTag("Player"))
            {
                Debug.Log("No obstacles between player and enemy. Fire!");
                attackController.enabled = true;
            }
        }
    }


    /* When the player is no longer detected, disable attack controller.
     */ 
    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        attackController.enabled = false;
    }


    /* Called when the enemy dies. Stops attempting to detect the player.
     */ 
    private void OnEnemyDied(GameObject enemy)
    {
        boxCollider.enabled = false;
    }
}
