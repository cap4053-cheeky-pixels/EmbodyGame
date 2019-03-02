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

    // Signals an enemy's health change; mainly used for the boss
    public delegate void HealthChanged();
    public event HealthChanged healthChangedEvent;

    
    /* Called before the game starts. Sets up all necessary info.
     */
    void Awake()
    {
    }
    
    
    /* Called every frame.
     */
    void Update()
    {
        
    }
    
    
    /* Called when an enemy has died. Spawns a heart with random probability.
     */
    void DropHeart()
    {
        
    }
    
    
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
    
    
    /* Called when this Enemy encounters another object.
     */
    private void OnTriggerEnter(Collider other)
    {
        // Detecting projectiles
        if(other.gameObject.CompareTag("PlayerProjectile") && Health != 0)
        {
            Projectile projectile = other.gameObject.GetComponent<Projectile>();
            ChangeHealthBy(-projectile.damage);
            Destroy(other.gameObject);
        }
    }

    private void OnEnemyDied()
    {
        // TODO add logic for heart drops
        isDead = true;
        isPossessable = true;
        // Signal the death of this enemy
        deathEvent?.Invoke(gameObject);
        // Call the death method on any appropriate controllers
        foreach (IOnDeathController odc in GetComponents<IOnDeathController>())
        {
            odc.OnDeath();
        }
    }
}
