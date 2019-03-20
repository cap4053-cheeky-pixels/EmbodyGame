using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class WanderMovement : MonoBehaviour, IOnDeathController
{
    private EnemyMovement movement;

    [Tooltip("The radius within which the enemy should be able to wander")]
    public float wanderRadius;


    /* Set up script.
     */ 
    private void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        StartCoroutine(Wander());
    }


    /* Allows the enemy to wander within the navmesh.
     */ 
    IEnumerator Wander()
    {
        NavMeshPath path;

        do
        {
            path = new NavMeshPath();

            Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
            randomDirection += transform.position;

            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, 1 << NavMesh.GetAreaFromName("Walkable"));

            Vector3 destination = hit.position;
            movement.agent.CalculatePath(destination, path);

            // This check is necessary to ensure that the agent only visits valid locations
            if(path.status != NavMeshPathStatus.PathInvalid)
            {
                // TODO: there's a random edge case where the position is still invalid: "CalculatePolygonPath: invalid target position { Infinity, Infinity, Infinity }"
                movement.MoveTo(destination);
            }

            // Some loss of time due to these computations, but negligible
            float distanceToDestination = Vector3.Distance(transform.position, destination);
            float timeToWaitBeforeRecalculation = distanceToDestination / movement.agent.speed; 

            yield return new WaitForSeconds(timeToWaitBeforeRecalculation);
        }
        while (enabled);
    }


    /* Shuts down the movement behavior when the enemy dies.
     */
    public void OnDeath()
    {
        enabled = false;
        movement.Stop();
    }
}
