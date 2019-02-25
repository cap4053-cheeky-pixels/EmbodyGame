using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    // This entity's current health (at most MaxHealth)
    public int Health;

    // The maximum health this entity can currently have
    public int MaxHealth;

    // TODO Remove FireWeapon logic as this will now live in ShootController.cs
    // The weapon this entity uses for attacking
    public GameObject weapon;

    // Reference to the script of the weapon
    protected Weapon attackingWeapon;

    // The model that's expected to be a child of this game object
    public GameObject model;
    public void SetModel(GameObject model) { this.model = model; }


    /* Sets this entity's weapon to the given GameObject.
       TODO Remove FireWeapon logic as this will now live in ShootController.cs
     */
    public void SetWeapon(GameObject weapon)
    {
        this.weapon = weapon;
        attackingWeapon = weapon.GetComponent<Weapon>();
    }


    // Changes this entity's maximum health by the given amount
    public abstract void ChangeMaxHealthBy(int amount);


    // Changes this entity's current health by the given amount
    public abstract void ChangeHealthBy(int amount);
}
