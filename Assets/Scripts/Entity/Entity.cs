using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    // This entity's current health (at most MaxHealth)
    public int Health { get; set; }

    // The maximum health this entity can currently have
    public int MaxHealth { get; set; }

    // The weapon this entity uses for attacking
    public GameObject weapon;
    
    // How quickly this entity moves
    public int Speed { get; set; }

    // Changes this entity's maximum health by the given amount
    public abstract void ChangeMaxHealthBy(int amount);

    // Changes this entity's current health by the given amount
    public abstract void ChangeHealthBy(int amount);
}
