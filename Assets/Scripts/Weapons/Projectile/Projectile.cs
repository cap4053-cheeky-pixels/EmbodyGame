using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public delegate void OnHitHandler(Vector3 position);
    public event OnHitHandler OnHit;
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
            hit();
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
            hit();
        }
    }

    /**
        Method that is called when the projectile hits something and will be destroyed
     */
    void hit()
    {
        // Notify any event listeners
        OnHit?.Invoke(transform.position);
        // Destroy the object
        Destroy(gameObject);
    }


    void DamageEnemy(GameObject enemy)
    {
        Enemy enemyController = enemy.GetComponent<Enemy>();
        // Only damage and destroy the projectile if the enemy is alive
        if (enemyController.IsDead()) return;

        enemyController.ChangeHealthBy(-damage);
        hit();
    }


    /* Once spawned and given a speed, a projectile will travel at its velocity
     */
    void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }
}
