using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    // Used to signal an enemy's death to the rooms that spawned them
    public delegate void Died(GameObject who);
    public event Died deathEvent;

    private bool isDead;
    public bool IsDead() { return isDead; }
    public bool isPossessable = false;

    public int collideDamage = 1;

    // Signals an enemy's health change; mainly used for the boss
    public delegate void HealthChanged();
    public event HealthChanged healthChangedEvent;


    /* Changes this Enemy's max health by the given amount.
     */
    public override void ChangeMaxHealthBy(int amount)
    {
        MaxHealth += amount;
    }


    /* Changes this Enemy's health by the given amount.
     */
    public override void ChangeHealthBy(int amount)
    {
        if (isDead) return;

        Health += amount;
        if (Health < 0) Health = 0;

        if(Health == 0)
        {
            OnEnemyDied();
        }
        else
        {
            healthChangedEvent?.Invoke();
        }
    }


    /* Handles collision with other objects of interest, specifically the player.
     */ 
    private void OnCollisionEnter(Collision other)
    {
        if (isDead || !other.gameObject.CompareTag("Player")) return;

        other.gameObject.GetComponent<Player>().ChangeHealthBy(-collideDamage);
    }

    
    /* Called when this enemy dies.
     */ 
    private void OnEnemyDied()
    {
        // Mark it as dead for any code that checks the status
        isDead = true;

        // Signal the death of this enemy
        deathEvent?.Invoke(gameObject);

        // Call Entity.OnDeath()
        OnDeath();
    }
}
