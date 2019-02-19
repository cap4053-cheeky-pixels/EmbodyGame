using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 velocity;
    public float damage = 1;


    /* Projectiles self-destruct when colliding with walls.
     */ 
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }


    // Once spawned and given a speed, a projectile will travel at its velocity
    void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }
}
