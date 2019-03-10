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
        // Damage enemy
        else if( (otherObject.CompareTag("Enemy") || otherObject.CompareTag("Boss")) && 
                  gameObject.CompareTag("PlayerProjectile"))
        {
            DamageEnemy(otherObject);
        }
        // Damage player
        else if (otherObject.CompareTag("Player") && gameObject.CompareTag("EnemyProjectile"))
        {
            // Damage player
            otherObject.GetComponent<Player>().ChangeHealthBy(-damage);
            Destroy(gameObject);
        }
    }


    void DamageEnemy(GameObject enemy)
    {
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
