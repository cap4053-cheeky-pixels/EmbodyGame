using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicLaserWeapon : ProjectileWeapon
{
    /* Fires this weapon's projectile with the given tag. The tag is used by other entities
     * for the purposes of inflicting damage to themselves when they collide with the projectile.
     */
    public override void Attack(string tag, GameObject target)
    {
        // Prevents continuous firing
        if (timer > timeBetweenAttacks)
        {
            // Spawn a projectile into the scene
            Vector3 spawnPos = new Vector3(0, upwardOffset, 0) + transform.position + transform.forward * forwardOffset;
            GameObject projectileInstance = Instantiate(projectile, spawnPos, transform.rotation);
            projectileInstance.tag = tag;
            projectileInstance.layer = LayerMask.NameToLayer(tag);

            // Set the projectile's velocity and damage properties via the associated script
            Projectile projectileScript = projectileInstance.GetComponent<Projectile>();
            projectileScript.velocity = transform.forward * speed;
            projectileScript.damage = damage;
            fireAudio.Play();
            // Destroy the projectile once its lifetime elapses
            Destroy(projectileInstance, projectileLifetime);

            // Reset the timer
            timer = 0;
        }
    }
}
