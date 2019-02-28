using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject enemyPrefab = null;

    // Used to customize the enemy that spawns at this point
    public int maxHealth;
    public float speed;
    public float scale = 1.0f;
    public float fireRate;
    public float yRotation = 0;
}
