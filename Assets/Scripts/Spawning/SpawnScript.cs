using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnScript : MonoBehaviour
{
    /* Loops through all children SpawnPoint objects and spawns enemies at those points.
     * Returns a HashSet of all spawned enemies for use by the rooms.
     */
    public HashSet<GameObject> SpawnEnemies()
    {
        HashSet<GameObject> spawned = new HashSet<GameObject>();

        // Loop through each spawn point
        foreach (Transform child in transform)
        {
            SpawnPoint spawnPoint = child.gameObject.GetComponent<SpawnPoint>();
            GameObject enemyToSpawn = spawnPoint.enemyPrefab;

            // If an enemy is set to spawn at that point, go ahead and instantiate it
            if (enemyToSpawn != null)
            {
                GameObject spawnedEnemy = Instantiate(enemyToSpawn, spawnPoint.transform.position, Quaternion.Euler(0, spawnPoint.yRotation, 0));
                Enemy script = spawnedEnemy.GetComponent<Enemy>();

                script.MaxHealth = spawnPoint.maxHealth;
                script.Health = spawnPoint.maxHealth;
                script.gameObject.GetComponent<NavMeshAgent>().speed = spawnPoint.speed;
                script.gameObject.transform.localScale *= spawnPoint.scale;
                // TODO uncomment once implemented script.attemptFirerate = spawnPoint.fireRate;

                spawned.Add(spawnedEnemy);
            }
        }

        return spawned;
    }
}
