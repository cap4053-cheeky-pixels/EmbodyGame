using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadshotWeapon : ProjectileWeapon
{
    // The angular offset from the forward vector at which the other shots should be fired
    private float angularOffset = 90;


    /* Fires this weapon's projectile with the given tag. The tag is used by other entities
     * for the purposes of inflicting damage to themselves when they collide with the projectile.
     */
    public override void Attack(string tag, GameObject target)
    {
        // Prevents continuous firing
        if (timer > timeBetweenAttacks)
        {
            // Fire 4 projectiles at "angularOffset" degrees displacement from center
            Vector3 forward = transform.forward.normalized;
            Vector3 forwardLeftPos = Quaternion.Euler(0, -angularOffset, 0) * forward;
            Vector3 forwardRightPos = Quaternion.Euler(0, angularOffset, 0) * forward;
            Vector3 backwardPos = Quaternion.Euler(0, angularOffset * 2, 0) * forward;
            Vector3 offset = new Vector3(0, upwardOffset, 0);

            // Specify the actual spawn locations for the four projectiles
            Vector3 spawnForwardPos = offset + transform.position + forward * forwardOffset;
            Vector3 spawnLeftPos = offset + transform.position + forwardLeftPos * forwardOffset;
            Vector3 spawnRightPos = offset + transform.position + forwardRightPos * forwardOffset;
            Vector3 spawnBackwardPos = offset + transform.position + backwardPos * forwardOffset;

            // Spawn the forward-facing projectile into the scene
            GameObject projectileInstance = Instantiate(projectile, spawnForwardPos, transform.rotation);
            projectileInstance.tag = tag;
            Projectile pro = projectileInstance.GetComponent<Projectile>();
            pro.velocity = forward * speed;
            pro.damage = damage;
            Destroy(projectileInstance, projectileLifetime);

            // Spawn the left-facing projectile into the scene
            GameObject projectileLeftInstance = Instantiate(projectile, spawnLeftPos, transform.rotation);
            projectileLeftInstance.tag = tag;
            Projectile proLeft = projectileLeftInstance.GetComponent<Projectile>();
            proLeft.velocity = forwardLeftPos * speed;
            proLeft.damage = damage;
            Destroy(projectileLeftInstance, projectileLifetime);

            // Spawn the right-facing projectile into the scene
            GameObject projectileRightInstance = Instantiate(projectile, spawnRightPos, transform.rotation);
            projectileRightInstance.tag = tag;
            Projectile proRight = projectileRightInstance.GetComponent<Projectile>();
            proRight.velocity = forwardRightPos * speed;
            proRight.damage = damage;
            Destroy(projectileRightInstance, projectileLifetime);

            // Spawn the backward-facing projectile into the scene
            GameObject projectileBackwardInstance = Instantiate(projectile, spawnBackwardPos, transform.rotation);
            projectileBackwardInstance.tag = tag;
            Projectile proBackward = projectileBackwardInstance.GetComponent<Projectile>();
            proBackward.velocity = backwardPos * speed;
            proBackward.damage = damage;
            Destroy(projectileBackwardInstance, projectileLifetime);

            // And of course, reset the timer
            timer = 0;
        }
    }
}
