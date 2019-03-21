using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FieldOfView : MonoBehaviour, IOnDeathController
{
    [Tooltip("The maximum distance at which targets can be sighted")]
    public float viewRadius;

    [Tooltip("The total field of view angle")]
    [Range(0, 360)] public float viewAngle;

    [Tooltip("The layer on which the player resides")]
    public LayerMask playerMask;

    [Tooltip("The layer on which the obstacles reside")]
    public LayerMask obstacleMask;

    [HideInInspector] public Transform player;

    [Tooltip("Behavior scripts, if any, that should be disabled when the player is detected")]
    [SerializeField] private MonoBehaviour[] behaviorsToDisableUponDetection;

    [Tooltip("Behavior scripts, if any, that should be enabled when the player is detected")]
    [SerializeField] private MonoBehaviour[] behaviorsToEnableUponDetection;


    /* Called before the first frame update.
     */
    private void Start()
    {
        player = null;
        gameObject.GetComponent<Enemy>().healthChangedEvent += OnEnemyHurt;
        StartCoroutine(FindPlayerWithDelay(0.2f));
    }

    
    /* Called when the enemy is hit by a player projectile. This is treated
     * as a detection.
     */ 
    private void OnEnemyHurt()
    {
        DisableBehaviors();
        EnableBehaviors();
        enabled = false;
    }


    /* Finds all visible targets within the player's field of view,
     * with the given delay between each operation.
     */
    private IEnumerator FindPlayerWithDelay(float delay)
    {
        while (enabled)
        {
            yield return new WaitForSeconds(delay);
            FindPlayer();
        }
    }


    /* Locates all visible targets within the player's field of view.
     */
    private void FindPlayer()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, playerMask);

        foreach (Collider targetCollider in targetsInViewRadius)
        {
            player = targetCollider.gameObject.transform;

            Vector3 playerDirection = (player.position - transform.position).normalized;
            float angleToPlayer = Vector3.Angle(transform.forward, playerDirection);

            // Player is within the FOV angle
            if (angleToPlayer < viewAngle / 2.0f)
            {
                float distanceToTarget = Vector3.Distance(transform.position, player.position);

                // If there is no obstacle in between us and the player, then we've found the player
                if (!Physics.Raycast(transform.position, playerDirection, distanceToTarget, obstacleMask))
                {
                    DisableBehaviors();
                    EnableBehaviors();

                    // Once the player is found, the enemy will pursue them and has no need for detection
                    enabled = false;
                }
            }
        }
    }


    /* Disables all pre-player-detection behaviors.
     */ 
    private void DisableBehaviors()
    {
        foreach (MonoBehaviour script in behaviorsToDisableUponDetection)
        {
            script.enabled = false;
        }
    }


    /* Enables all post-player-detection behaviors.
     */ 
    private void EnableBehaviors()
    {
        foreach (MonoBehaviour script in behaviorsToEnableUponDetection)
        {
            script.enabled = true;
        }
    }


    /* Returns a normal vector representing the direction of the given angle.
     */
    public Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        // If the given angle is relative to the player, transform it to global as seen from above (looking opposite the y axis)
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }

        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;

        // Note: cosine and sine are effectively swapped in Unity
        // Moreover, keep in mind that we're in the xz plane
        return new Vector3(Mathf.Sin(angleInRadians), 0, Mathf.Cos(angleInRadians)).normalized;
    }


    /* Called when the associated enemy dies.
     */
    public void OnDeath()
    {
        enabled = false;
    }
}
