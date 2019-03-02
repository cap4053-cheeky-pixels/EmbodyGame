using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Used by the AnimationController to trigger events.
 */ 
public class EventHandler : MonoBehaviour
{
    // The ranged weapon the devil uses to fire projectiles
    [SerializeField] private ProjectileWeapon rangedWeapon;


    /* Called at a specific point in the attack_2 animation to sync with the devil
     * thrusting his hand forward and summoning a projectile.
     */  
    public void ThrowFireball()
    {
        rangedWeapon.Attack("EnemyProjectile");
    }
}
