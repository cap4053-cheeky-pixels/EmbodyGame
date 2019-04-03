using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class WanderMovement : MonoBehaviour, IOnDeathController
{
    private EnemyMovement movement;

    [Tooltip("The radius within which the enemy should be able to wander")]
    public float wanderRadius;

    [Tooltip("The minimum number of seconds for which an enemy should stop once they've reached their destination")]
    [SerializeField] private float minSecondsToWaitAtDestination = 0f;

    [Tooltip("The maximum number of seconds for which an enemy should stop once they've reached their destination")]
    [SerializeField] private float maxSecondsToWaitAtDestination = 2f;


    /* Set up script.
     */
    private void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        StartCoroutine(Wander());
    }


    /* Generates a random direction in which the agent should head.
     */  
    Vector3 GenerateDestination()
    {
        // Pick a random direction for the agent to head in
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;

        // Sample a point on the navmesh
        NavMeshHit navMeshHit;
        int areaMask = 1 << NavMesh.GetAreaFromName("Walkable");
        NavMesh.SamplePosition(randomDirection, out navMeshHit, wanderRadius, areaMask);

        // Handles an edge case when a NavMeshHit is generated for an illegal position
        if (!navMeshHit.hit) return transform.position;

        // Otherwise, if the hit was valid, return that as the new destination
        return navMeshHit.position;
    }
    

    /* Allows the enemy to wander within the navmesh.
     */ 
    IEnumerator Wander()
    {
        NavMeshPath path;

        // Run this code only as long as the wander behavior is enabled
        while(enabled)
        {
            path = new NavMeshPath();

            // Calculate a path to the destination 
            Vector3 destination = GenerateDestination();
            movement.agent.CalculatePath(destination, path);

            // Ensure that the agent only visits valid locations
            if (path.status != NavMeshPathStatus.PathInvalid)
            {
                // Move the agent there
                movement.MoveTo(destination);

                // Some loss of time due to these computations, but negligible
                float distanceToDestination = Vector3.Distance(transform.position, destination);
                float timeToWaitBeforeRecalculation = 0.0f;

                if (movement.agent.speed != 0.0f)
                {
                    timeToWaitBeforeRecalculation = distanceToDestination / movement.agent.speed;
                }

                // Don't proceed until the enemy has arrived at its destination
                yield return new WaitForSeconds(timeToWaitBeforeRecalculation);

                // Then wait for a randomized number of seconds between min and max
                float idleTime = Random.Range(minSecondsToWaitAtDestination, maxSecondsToWaitAtDestination);
                yield return new WaitForSeconds(idleTime);
            }

            // If a valid path was not generated, just resume on the next frame update
            else
            {
                yield return null;
            }
        }
    }


    /* Shuts down the movement behavior when the enemy dies.
     */
    public void OnDeath()
    {
        enabled = false;
        movement.Stop();
    }
}
