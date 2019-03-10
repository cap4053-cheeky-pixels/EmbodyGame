using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 velocity;
    public int damage = 1;


    /* Projectiles self-destruct when colliding with walls.
     */ 
    void OnTriggerEnter(Collider other)
    {
        GameObject otherObject = other.gameObject;

        // Disappear when hitting walls
        if (otherObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        else if(otherObject.CompareTag("Enemy"))
        {
            // Damage enemy
            HitWithEnemy(otherObject);
        }
        else if (otherObject.CompareTag("Player") && gameObject.CompareTag("EnemyProjectile"))
        {
            // Damage player
            otherObject.GetComponent<Player>().ChangeHealthBy(-damage);
            Destroy(gameObject);
        }
    }

    void HitWithEnemy(GameObject enemy)
    {
        // If this gameobject is not a player projectile, return
        if (!gameObject.CompareTag("PlayerProjectile")) return;

        Enemy enemyController = enemy.GetComponent<Enemy>();
        if (enemyController.IsDead()) return;

        enemyController.ChangeHealthBy(-damage);
        Destroy(gameObject);
    }


    /* Once spawned and given a speed, a projectile will travel at its velocity
     */ 
    void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }
}
