using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(FieldOfView))]
public class DetectPlayer : MonoBehaviour
{
    // The field of view this enemy uses to detect the player
    private FieldOfView fov;

    [Tooltip("Behavior scripts, if any, that should be disabled when the player is detected")]
    [SerializeField] private MonoBehaviour[] behaviorsToDisableUponDetection;

    [Tooltip("Behavior scripts, if any, that should be enabled when the player is detected")]
    [SerializeField] private MonoBehaviour[] behaviorsToEnableUponDetection;

    private const float viewRadiusAfterPlayerDetected = 40;


    /* Called before the first frame update.
     */
    private void Start()
    {
        fov = GetComponent<FieldOfView>();
        GetComponent<Enemy>().healthChangedEvent += OnEnemyHurt;
        StartCoroutine(FindPlayerWithDelay(0.2f));
    }


    /* Facilitates a transition from one set of behaviors (prior to detection of the player)
     * to another set of behaviors (after detection of the player).
     */ 
    private void TransitionToSecondaryState()
    {
        // State transitions (turn off one switch, turn on another)
        DisableBehaviors();
        EnableBehaviors();

        // Ensuring that the enemy can "see" the entire room
        fov.viewRadius = viewRadiusAfterPlayerDetected;

        // Stops the coroutine used to detect the player
        enabled = false;
    }



    /* Called when the enemy is hit by a player projectile. This is treated
     * as a detection.
     */
    private void OnEnemyHurt()
    {
        TransitionToSecondaryState();
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


    /* Looks for the player character with the given delay in seconds
     * between each search.
     */ 
    private IEnumerator FindPlayerWithDelay(float delay)
    {
        while (enabled)
        {
            if(fov.PlayerWithinView())
            {
                TransitionToSecondaryState();
            }

            yield return new WaitForSeconds(delay);
        }
    }
}
