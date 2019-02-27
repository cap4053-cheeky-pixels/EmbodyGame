using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeleeWeapon : Weapon
{
    public GameObject weapon;


    public override void Attack(string tag, GameObject target)
    {
        // Damage enemies
        if(tag == "PlayerMeleeWeapon" && target.CompareTag("Enemy"))
        {
            target.GetComponent<Enemy>().ChangeHealthBy(-damage);
        }

        // Damage player
        else if(tag == "EnemyMeleeWeapon" && target.CompareTag("Player"))
        {
            target.GetComponent<Player>().ChangeHealthBy(-damage);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        Attack(gameObject.tag, other.gameObject);
    }
}
