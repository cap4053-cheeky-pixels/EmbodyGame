using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireboltLauncher : ProjectileWeapon
{
    public float angularOffset;
    public float tripleProbability;
    public float circleProbability;

    const int angleBetweenBlasts = 45;

    /* Fires this weapon's projectile with the given tag. The tag is used by other entities
     * for the purposes of inflicting damage to themselves when they collide with the projectile.
     */
    public override void Attack(string tag, GameObject target)
    {
        // Prevents continuous firing
        if (timer > timeBetweenAttacks)
        {
            // Randomly choose between other kinds of attacks
            var rand = Random.value;
            if(rand < circleProbability)
            {
                // spawn eight blasts around a circle starting from a random point
                var randPoint = Random.Range(0,360);
                for (int i = 0; i < 8; i++)
                {
                    var point = randPoint + (i * angleBetweenBlasts);

                    Vector3 spawnPos = new Vector3(0, upwardOffset, 0) + transform.position + transform.forward * forwardOffset;
                    GameObject projectileInstance = Instantiate(projectile, spawnPos, Quaternion.Euler(0, point, 0));
                    projectileInstance.tag = tag;
                    Firebolt firebolt = projectileInstance.GetComponent<Firebolt>();
                    firebolt.damage = damage;
                    Destroy(projectileInstance, projectileLifetime);
                }
            }
            else if (rand < tripleProbability)
            {
                Vector3 forward = transform.forward;
                Vector3 offset = new Vector3(0, upwardOffset, 0);
                Vector3 spawnPos = offset + transform.position + forward * forwardOffset;

                // Spawn the forward-facing projectile into the scene
                GameObject projectileInstance = Instantiate(projectile, spawnPos, transform.rotation);
                projectileInstance.tag = tag;
                Firebolt pro = projectileInstance.GetComponent<Firebolt>();
                pro.damage = damage;
                Destroy(projectileInstance, projectileLifetime);

                // Spawn the left projectile into the scene at its angle offset
                GameObject projectileLeftInstance = Instantiate(projectile, spawnPos, Quaternion.Euler(0, -angularOffset, 0) * transform.rotation);
                projectileLeftInstance.tag = tag;
                Firebolt proLeft = projectileLeftInstance.GetComponent<Firebolt>();
                proLeft.damage = damage;
                Destroy(projectileLeftInstance, projectileLifetime);

                // Spawn the right projectile into the scene at its angle offset
                GameObject projectileRightInstance = Instantiate(projectile, spawnPos, Quaternion.Euler(0, angularOffset, 0) * transform.rotation);
                projectileRightInstance.tag = tag;
                Firebolt proRight = projectileRightInstance.GetComponent<Firebolt>();
                proRight.damage = damage;
                Destroy(projectileRightInstance, projectileLifetime);
            }
            else
            {
                // Spawn a projectile into the scene
                Vector3 spawnPos = new Vector3(0, upwardOffset, 0) + transform.position + transform.forward * forwardOffset;
                GameObject projectileInstance = Instantiate(projectile, spawnPos, transform.rotation);
                projectileInstance.tag = tag;

                // Set the projectile's velocity and damage properties via the associated script
                Firebolt firebolt = projectileInstance.GetComponent<Firebolt>();
                firebolt.damage = damage;

                // Destroy the projectile once its lifetime elapses
                Destroy(projectileInstance, projectileLifetime);
            }

            // Reset the timer
            timer = 0;
        }
    }
}