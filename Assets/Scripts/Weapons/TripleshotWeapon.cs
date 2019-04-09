using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleshotWeapon : ProjectileWeapon
{
    // The angular offset from the forward vector at which the other two shots should be fired
    public float angularOffset;


    /* Fires this weapon's projectile with the given tag. The tag is used by other entities
     * for the purposes of inflicting damage to themselves when they collide with the projectile.
     */
    public override void Attack(string tag, GameObject target)
    {
        // Prevents continuous firing
        if (timer > timeBetweenAttacks)
        {
            // We'll fire 3 projectiles at "angularOffset" degrees displacement from center
            Vector3 forward = transform.forward;
            Vector3 forwardLeftPos = Quaternion.Euler(0, -angularOffset, 0) * forward;
            Vector3 forwardRightPos = Quaternion.Euler(0, angularOffset, 0) * forward;
            Vector3 offset = new Vector3(0, upwardOffset, 0);

            // Specify the actual spawn locations for the three projectiles
            Vector3 spawnForwardPos = offset + transform.position + forward * forwardOffset;
            Vector3 spawnLeftPos = offset + transform.position + forwardLeftPos * forwardOffset;
            Vector3 spawnRightPos = offset + transform.position + forwardRightPos * forwardOffset;

            // Spawn the forward-facing projectile into the scene
            GameObject projectileInstance = Instantiate(projectile, spawnForwardPos, transform.rotation);
            projectileInstance.tag = tag;
            Projectile pro = projectileInstance.GetComponent<Projectile>();
            pro.velocity = forward * speed;
            pro.damage = damage;
            Destroy(projectileInstance, projectileLifetime);

            // Spawn the left projectile into the scene at its angle offset
            GameObject projectileLeftInstance = Instantiate(projectile, spawnLeftPos, transform.rotation);
            projectileLeftInstance.tag = tag;
            Projectile proLeft = projectileLeftInstance.GetComponent<Projectile>();
            proLeft.velocity = forwardLeftPos * speed;
            proLeft.damage = damage;
            Destroy(projectileLeftInstance, projectileLifetime);

            // Spawn the right projectile into the scene at its angle offset
            GameObject projectileRightInstance = Instantiate(projectile, spawnForwardPos, transform.rotation);
            if(transform.parent.gameObject.tag == "Player")
            fireAudio.Play();
            projectileRightInstance.tag = tag;
            Projectile proRight = projectileRightInstance.GetComponent<Projectile>();
            proRight.velocity = forwardRightPos * speed;
            proRight.damage = damage;
            Destroy(projectileRightInstance, projectileLifetime);

            // And of course, reset the timer
            timer = 0;
        }
    }
}
