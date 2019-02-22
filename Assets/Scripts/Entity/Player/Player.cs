using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    // Used to signal health change events to the heart container system
    public delegate void HealthChanged();
    public event HealthChanged healthChangedEvent;

    // Mainly used when pausing the game
    private bool actionsEnabled;
    public void SetEnabled(bool enabled) { actionsEnabled = enabled; }
    public bool ActionsEnabled() { return actionsEnabled; }


    /* Called before the game starts. Sets up all necessary info.
     */ 
    void Awake()
    {
        SetEnabled(true);
        SetWeapon(weapon);
        healthChangedEvent?.Invoke();
    }


    /* Changes this Player's max health by the given amount.
     */ 
    public override void ChangeMaxHealthBy(int amount)
    {
        MaxHealth += amount;
        healthChangedEvent?.Invoke();
    }


    /* Changes this Player's health by the given amount.
     */ 
    public override void ChangeHealthBy(int amount)
    {
        Health += amount;
        healthChangedEvent?.Invoke();

        // Player died
        if (Health == 0)
        {
            // SceneManager.LoadScene(0) // TODO make scene 0 be game over, uncomment this when done
        }
    }


    /* Allows this Player to fire its weapon, assuming it has one.
     */ 
    public void FireWeapon()
    {
        if (fireableWeapon != null)
        {
            Debug.Log("???");
            fireableWeapon.Fire("PlayerProjectile");
        }
    }


    /* Called when this Player encounters another object.
     */
    private void OnTriggerEnter(Collider other)
    {
        // Detecting projectiles
        if (other.gameObject.CompareTag("EnemyProjectile") && Health != 0)
        {
            Projectile projectile = other.gameObject.GetComponent<Projectile>();
            ChangeHealthBy(projectile.damage);
            Destroy(other.gameObject);
        }
    }
}
