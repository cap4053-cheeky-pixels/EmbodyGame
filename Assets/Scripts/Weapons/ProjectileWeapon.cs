using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileWeapon : Weapon
{
    // A reference to the projectile this weapon fires
    public GameObject projectile;

    // The speed at which this weapon's projectiles are fired
    public float speed;

    // The number of seconds until a projectile is automatically destroyed after spawning
    public float projectileLifetime;

    // The offset for where the projectile should spawn in front of the entity
    public float forwardOffset;

    // The offset for where the projectile should spawn above ground
    public float upwardOffset;
}
