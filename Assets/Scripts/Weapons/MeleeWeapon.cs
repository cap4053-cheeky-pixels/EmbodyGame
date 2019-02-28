using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeleeWeapon : Weapon
{
    // The weapon used for attacking
    public GameObject weapon;

    // Audio that plays for melee attacks
    public AudioSource meleeSound;


    /* Overriden method inherited from Weapon.
     */ 
    public override void Attack(string tag, GameObject target)
    {
        // Damage enemies
        if(target.CompareTag("Enemy") && tag == "PlayerMeleeWeapon")
        {
            meleeSound.Play();
            target.GetComponent<Enemy>().ChangeHealthBy(-damage);
        }

        // Damage player
        else if(target.CompareTag("Player") && tag == "EnemyMeleeWeapon")
        {
            meleeSound.Play();
            target.GetComponent<Player>().ChangeHealthBy(-damage);
        }
    }


    /* Called when the weapon interacts with other objects. Plays the
     * melee audio clip and initiates the attack.
     */ 
    private void OnTriggerEnter(Collider other)
    {
        Attack(gameObject.tag, other.gameObject);
    }
}
