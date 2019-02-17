using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    public List<GameObject> doors;      // all doors this room is responsible for opening/closing
    public float width = 30;            // x scaling (30 by default)
    public float length = 30;           // z scaling (30 by default)

    private HashSet<GameObject> spawnedEnemies;
    private SpawnScript spawner;
    private WaitForSeconds wait;
    private int numEnemies = 0;


    /* Sets up the room by spawning enemies and subscribing to all their death events.
     */
    private void Awake()
    {
        SetDimensions(width, length);
        spawner = gameObject.transform.Find("SpawnPoints").gameObject.GetComponent<SpawnScript>();
        spawnedEnemies = spawner.SpawnEnemies();
        numEnemies = spawnedEnemies.Count;
        SubscribeToEnemyDeath();
    }


    /* Sets the width (x scaling) and length (z scaling) of this room to the given values.
     */ 
    public void SetDimensions(float x, float z)
    {
        gameObject.transform.localScale = new Vector3(x, gameObject.transform.localScale.y, z);
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
        foreach (GameObject door in doors)
        {
            // DoorController doorController = door.GetComponent<DoorController>(); // TODO re-enable once implemented
            // doorController.Open();
        }
    }


    /* Closes all doors for the current room.
     */
    void LockAllDoors()
    {
        foreach (GameObject door in doors)
        {
            // DoorController doorController = door.GetComponent<DoorController>(); // TODO re-enable once implemented
            // doorController.Close();
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
