﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWeapon : MonoBehaviour, IWeapon
{
    public GameObject projectile;
    public float speed = 15;
    public float timeBetweenShots = 1;
    public float projectileLifetime = 10;
    public float damage = 1;
    private float timer = 0;
    public float forwardOffset = 1.5f;
    public float yOffset = 1.5f;


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }


    /* Fires this weapon's projectile with the given tag. The tag is used by other entities
     * for the purposes of inflicting damage to themselves when they collide with the projectile.
     */ 
    public void Fire(string tag)
    {
        // Prevents continuous firing
        if (timer > timeBetweenShots)
        {
            // Spawn a projectile into the scene
            Vector3 spawnPos = new Vector3(0, yOffset, 0) + transform.position + transform.forward * forwardOffset;
            GameObject projectileInstance = Instantiate(projectile, spawnPos, transform.rotation);
            projectileInstance.tag = tag;

            // Set the projectile's velocity and damage properties via the associated script
            Projectile projectileScript = projectileInstance.GetComponent<Projectile>();
            projectileScript.velocity = transform.forward * speed;
            projectileScript.damage = damage;

            // Destroy the projectile once its lifetime elapses
            Destroy(projectileInstance, projectileLifetime);

            // Be sure to reset the timer so a new shot can be fired after timeBetweenShots seconds elapse
            timer = 0;
        }
    }
}
