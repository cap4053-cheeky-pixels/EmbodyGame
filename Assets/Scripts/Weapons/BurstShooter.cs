using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstShooter : ProjectileWeapon
{
    public int burstCount = 3;
    public float burstIntervalSec = 0.2f;

    IEnumerator burstRoutine;
    float burstStartTime;

    public override void Attack(string tag, GameObject target)
    {
        if (timer < timeBetweenAttacks) return;

        if (burstRoutine != null) StopCoroutine(burstRoutine);
        burstRoutine = BurstCoroutine(tag, transform.forward);
        StartCoroutine(burstRoutine);
        if(gameObject.tag == "Player")
        fireAudio.Play();
        // Reset the timer
        timer = 0;
    }

    IEnumerator BurstCoroutine(string tag, Vector3 direction)
    {
        burstStartTime = timer;
        for (int i = 0; i < burstCount; i++)
        {
            FireProjectile(tag, direction);
            yield return new WaitForSeconds(burstIntervalSec);
        }

        yield return null;
    }

    void FireProjectile(string tag, Vector3 direction)
    {
        // Spawn a projectile into the scene
        Vector3 spawnPos = new Vector3(0, upwardOffset, 0) + transform.position + direction * forwardOffset;
        GameObject projectileInstance = Instantiate(projectile, spawnPos, transform.rotation);
        projectileInstance.tag = tag;

        // Set the projectile's velocity and damage properties via the associated script
        Projectile projectileScript = projectileInstance.GetComponent<Projectile>();
        projectileScript.velocity = direction * speed;
        projectileScript.damage = damage;

        // Destroy the projectile once its lifetime elapses
        Destroy(projectileInstance, projectileLifetime);
    }
}
