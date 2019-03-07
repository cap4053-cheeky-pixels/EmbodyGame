using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class RoomScript : MonoBehaviour
{
    public List<GameObject> doors = new List<GameObject>();
    [SerializeField] private AudioSource doorAudio;

    private CameraController cameraController;
    private float desiredCameraHeight;

    private HashSet<GameObject> spawnedEnemies;
    [SerializeField] GameObject spawnerObject;
    private Spawner spawner;
    private int numEnemies;

    private bool playerWasHereBefore;


    /* Sets up the room.
     */
    private void Awake()
    {
        spawner = spawnerObject.GetComponent<Spawner>();
        spawner.spawningComplete += OnEnemiesSpawned;
        playerWasHereBefore = false;        

        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        desiredCameraHeight = 19.7f;
    }

    
    /* Called when the associated spawner object has completed spawning enemies
     * and has emitted its spawningComplete event.
     */ 
    private void OnEnemiesSpawned(HashSet<GameObject> enemies)
    {
        spawnedEnemies = enemies;
        numEnemies = spawnedEnemies.Count;
        SubscribeToEnemyDeath();
    }


    /* Allows this Room to listen to each enemy it has spawned in order to
     * detect enemy death. This allows it to control when the room doors open.
     */
    private void SubscribeToEnemyDeath()
    {
        // Loop through each enemy
        foreach (GameObject enemyObject in spawnedEnemies)
        {
            Enemy enemy = enemyObject.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.deathEvent += OnEnemyDied;
            }
        }
    }

    /* Called each time an enemy dies in the current room. Used to update the
     * enemy count and to handle the case when all enemies have been slain.
     */
    private void OnEnemyDied(GameObject enemy)
    {
        spawnedEnemies.Remove(enemy);
        numEnemies--;

        // When all enemies in the room have been slain
        if (numEnemies == 0)
        {
            doorAudio.Play();
            OpenAllDoors();            
        }
    }


    /* Opens all doors for the current room.
     */
    private void OpenAllDoors()
    {
        // If we have doors, open them
        if(doors.Count != 0)
        {
            foreach (GameObject door in doors)
            {
                if(door != null)
                {
                    DoorController doorController = door.GetComponent<DoorController>();
                    doorController.Open();
                }
            }
        }
        else
        {
            Debug.Log("No doors assigned");
        }        
    }


    /* Closes all doors for the current room.
     */
    private void CloseAllDoors()
    {
        // If we have doors, lock them
        if(doors.Count != 0)
        {
            foreach (GameObject door in doors)
            {
                if(door != null)
                {
                    DoorController doorController = door.GetComponent<DoorController>();
                    doorController.Close();
                }                
            }
        }
        else
        {
            Debug.Log("No doors assigned");
        }        
    }


    /* Called when any other collision object enters this Room. Used to detect when the player
     * enters the room. If the player is entering the room for the first time and there are
     * enemies to spawn, then the room spawns them. Also regulates door locking/unlocking.
     */
    private void OnTriggerEnter(Collider other)
    {
        // We only care about the player entering the room
        if (other.gameObject.tag == "Player")
        {
            // Move the camera to this room's center
            cameraController.MoveTo(transform.position + new Vector3(0, desiredCameraHeight, 0));

            // If this is the first time the player is entering this room
            if(!playerWasHereBefore)
            {
                // And there are enemies to spawn
                if(spawner.Size() > 0)
                {
                    doorAudio.Play();
                    CloseAllDoors();
                    spawner.SpawnEnemies();
                }
                // This will only be the case for rooms that have no enemies
                else
                {
                    OpenAllDoors();

                    // That is, if there are other doors in the room
                    // besides the one the player just went through
                    // Otherwise, don't just play the audio for the open
                    // door you went in through... Lol
                    if(doors.Count > 1)
                    {
                        doorAudio.Play();
                    }
                }

                playerWasHereBefore = true;
            }
            // If instead the player has re-entered this room
            else
            {
                // Logically speaking, this seems a little paradoxical, but we need this check
                // because the room's trigger collider is slightly smaller than the room itself
                // Thus, it's possible that the player could "re-enter" a room from within the room
                if(numEnemies == 0)
                {
                    OpenAllDoors();
                }
            }
        }
    }
}
