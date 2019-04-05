using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public List<GameObject> enemyPrefabs = new List<GameObject>();

    // Used to customize the enemy that spawns at this point
    public float scale = 1.0f;
    public float yRotation = 0;

    /**
     * Returns a random enemy to spawn from the internal pool of candidates.
     */
    public GameObject GetRandomEnemyToSpawn()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        int index = Random.Range(0, enemyPrefabs.Count);

        return enemyPrefabs[index];
    }
}
