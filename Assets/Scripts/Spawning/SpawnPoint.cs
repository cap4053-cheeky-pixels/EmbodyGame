using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject enemyPrefab = null;

    // Used to customize the enemy that spawns at this point
    public int maxHealth = 2;
    public int speed = 1;
    public float fireRate = 0.5f;
    public float yRotation = 0;
}
