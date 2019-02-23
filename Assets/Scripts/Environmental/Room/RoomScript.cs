using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    public List<GameObject> doors = new List<GameObject>();      // all doors this room is responsible for opening/closing
    private HashSet<GameObject> spawnedEnemies;
    private SpawnScript spawner;
    private WaitForSeconds wait;
    private int numEnemies = 0;


    /* Sets up the room by spawning enemies and subscribing to all their death events.
     */
    private void Awake()
    {
        spawner = gameObject.transform.Find("SpawnPoints").gameObject.GetComponent<SpawnScript>();
        spawnedEnemies = spawner.SpawnEnemies();
        numEnemies = spawnedEnemies.Count;
        SubscribeToEnemyDeath();
    }


    /* Allows this Room to listen to each enemy it has spawned in order to
     * detect enemy death. This allows it to control when the room doors open.
     */
    void SubscribeToEnemyDeath()
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
    void OnEnemyDied(GameObject enemy)
    {
        // enemy.GetComponent<Enemy>().SetMovement(false); // TODO re-enable once implemented
        spawnedEnemies.Remove(enemy);
        numEnemies--;

        if (numEnemies == 0)
        {
            UnlockAllDoors();
        }
    }


    /* Opens all doors for the current room.
     */
    void UnlockAllDoors()
    {
        if(doors != null)
        {
            foreach (GameObject door in doors)
            {
                DoorController doorController = door.GetComponent<DoorController>(); // TODO re-enable once implemented
                doorController.Open();
            }
        }
        else
        {
            Debug.Log("No doors assigned");
        }        
    }


    /* Closes all doors for the current room.
     */
    void LockAllDoors()
    {
        if(doors != null)
        {
            foreach (GameObject door in doors)
            {
                DoorController doorController = door.GetComponent<DoorController>(); // TODO re-enable once implemented
                doorController.Close();
            }
        }
        else
        {
            Debug.Log("No doors assigned");
        }        
    }


    /* Loops through all live enemies in the room and dispatches them to go follow
     * the player.
     */
    void DispatchEnemiesToFollowPlayer()
    {
        foreach (GameObject enemyObject in spawnedEnemies)
        {
            Enemy enemy = enemyObject.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                // The enemy's update loop condition will then be true
                // enemy.SetMovement(true); // TODO re-enable once implemented
            }
        }
    }


    /* Called when any other collision object enters this Room. Used to detect when the player
     * enters the room. If there are currently enemies, it will lock all doors.
     */
    private void OnTriggerEnter(Collider c)
    {
        // When the player enters this room
        if (c.gameObject.tag == "Player")
        {
            // If there are enemies remaining, lock all doors and have enemies follow player
            if (numEnemies != 0)
            {
                LockAllDoors();
                DispatchEnemiesToFollowPlayer();
            }
            // If player entered an empty room, keep doors unlocked
            else
            {
                UnlockAllDoors();
            }
        }
    }
}
