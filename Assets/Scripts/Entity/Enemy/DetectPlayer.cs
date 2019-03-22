using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(FieldOfView))]
public class DetectPlayer : MonoBehaviour
{
    private FieldOfView fov;

    [Tooltip("Behavior scripts, if any, that should be disabled when the player is detected")]
    [SerializeField] private MonoBehaviour[] behaviorsToDisableUponDetection;

    [Tooltip("Behavior scripts, if any, that should be enabled when the player is detected")]
    [SerializeField] private MonoBehaviour[] behaviorsToEnableUponDetection;


    /* Called before the first frame update.
     */
    private void Start()
    {
        fov = GetComponent<FieldOfView>();
        GetComponent<Enemy>().healthChangedEvent += OnEnemyHurt;
        StartCoroutine(FindPlayerWithDelay(0.2f));
    }


    /* Called when the enemy is hit by a player projectile. This is treated
     * as a detection.
     */
    private void OnEnemyHurt()
    {
        DisableBehaviors();
        EnableBehaviors();
        fov.viewRadius = 40;
        enabled = false;
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
                DisableBehaviors();
                EnableBehaviors();
                fov.viewRadius = 40;
                enabled = false;
            }

            yield return new WaitForSeconds(delay);
        }
    }
}
