using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    // This entity's current health (at most MaxHealth)
    public int Health;

    // The maximum health this entity can currently have
    public int MaxHealth;

    // The weapon this entity uses for attacking
    public GameObject weapon;

    // How quickly this entity moves
    public int Speed;

    // TODO uncomment once IWeapon re-implemented
    //protected IWeapon fireableWeapon;

    /* Sets this entity's weapon to the given GameObject.
     */
    public void SetWeapon(GameObject weapon)
    {
        this.weapon = weapon;
        //fireableWeapon = this.weapon.GetComponent<IWeapon>(); // TODO uncomment once implemented
    }

    // Changes this entity's maximum health by the given amount
    public abstract void ChangeMaxHealthBy(int amount);

    // Changes this entity's current health by the given amount
    public abstract void ChangeHealthBy(int amount);
}
