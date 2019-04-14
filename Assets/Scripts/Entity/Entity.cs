using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    // This entity's current health (at most MaxHealth)
    public int Health;

    // The maximum health this entity can currently have
    public int MaxHealth;

    // The Audio that should play upon entity death
    public AudioSource deathAudio;
    
    // The Audio that should play when this entity incurs damage
    public AudioSource damageAudio;
    
    // The model that's expected to be a child of this game object
    public GameObject model;
    public void SetModel(GameObject model) { this.model = model; }

    // Changes this entity's maximum health by the given amount
    public abstract void ChangeMaxHealthBy(int amount);

    // Changes this entity's current health by the given amount
    public abstract void ChangeHealthBy(int amount);

    // Method should be called on entity death to propegate to components
    public void OnDeath()
    {
        // Call the death method on any appropriate controllers
        foreach (IOnDeathController odc in GetComponents<IOnDeathController>())
        {
            odc.OnDeath();
        }
    }
}
