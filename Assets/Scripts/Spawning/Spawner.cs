using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Spawner : MonoBehaviour
{
    // The points we'll poll for spawning
    private List<SpawnPoint> spawnPoints;
    [SerializeField] private AudioSource spawnAudio;
    [SerializeField] private float timeUntilSpawn;

    // Used for signaling the completion of spawning routine
    public delegate void Spawned(HashSet<GameObject> enemies);
    public event Spawned spawningComplete;


    /* Sets up the spawner with all of its spawn points.
     */ 
    private void Awake()
    {
        spawnPoints = new List<SpawnPoint>();

        // Loop through each child of the game object
        // Note that this assumes the SpawnPoints object will only have SpawnPoint children
        foreach (Transform child in transform)
        {
            SpawnPoint spawnPoint = child.gameObject.GetComponent<SpawnPoint>();
            spawnPoints.Add(spawnPoint);
        }
    }


    /* Returns the number of spawn points.
     */ 
    public int Size()
    {
        return spawnPoints.Count;
    }

    
    /* Helper coroutine that initiates the entire spawning process.
     */ 
    private IEnumerator SpawnWithDelay()
    {
        // Delay spawning by this many seconds
        yield return new WaitForSeconds(timeUntilSpawn);

        // Then, if we have something to spawn, play the audio and spawn
        if(Size() > 0)
        {
            spawnAudio.Play();
            Spawn();
        }
    }


    /* Actually spawns the enemies in. Helper function used by SpawnEnemies.
     * Loops through all children SpawnPoint objects and spawns enemies at those points.
     * Signals the completion of spawning, passing along a hash set of all enemies.
     */
    private void Spawn()
    {
        HashSet<GameObject> spawned = new HashSet<GameObject>();

        // Loop through each spawn point
        foreach (SpawnPoint spawnPoint in spawnPoints)
        {
            GameObject enemyToSpawn = spawnPoint.enemyPrefab;

            // If an enemy is set to spawn at that point, go ahead and instantiate it
            if (enemyToSpawn != null)
            {
                GameObject spawnedEnemy = Instantiate(enemyToSpawn, spawnPoint.transform.position, 
                                                      Quaternion.Euler(0, spawnPoint.yRotation, 0));
                Enemy script = spawnedEnemy.GetComponent<Enemy>();

                script.MaxHealth = spawnPoint.maxHealth;
                script.Health = spawnPoint.maxHealth;
                script.gameObject.GetComponent<NavMeshAgent>().speed = spawnPoint.speed;
                script.gameObject.transform.localScale *= spawnPoint.scale;
                spawned.Add(spawnedEnemy);
            }
        }

        spawningComplete?.Invoke(spawned);
        HideSpawnPoints();
    }


    /* Disables each spawn point so it no longer appears in game.
     */ 
    private void HideSpawnPoints()
    {
        foreach(SpawnPoint spawnPoint in spawnPoints)
        {
            spawnPoint.gameObject.SetActive(false);
        }
    }


    /* Public-facing function to be called by RoomScript.cs.
     */
    public void SpawnEnemies()
    {
        StartCoroutine(SpawnWithDelay());
    }
}
