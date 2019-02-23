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

    public float invincibilityDurationSeconds;
    private bool invincible = false;
    private MeshRenderer playerRenderer;


    /* Called before the game starts. Sets up all necessary info.
     */ 
    void Awake()
    {
        GameObject model = gameObject.transform.Find("Model").gameObject;
        playerRenderer = model.transform.Find("Ghost").gameObject.GetComponent<MeshRenderer>();
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
        // Start the invincibility coroutine if we take damage
        if(amount < 0)
        {
            StartCoroutine(BecomeTemporarilyInvincible());
        }

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
            fireableWeapon.Fire("PlayerProjectile");
        }
    }

    
    /* Called when this Player encounters another object.
     */
    private void OnCollisionEnter(Collision other)
    {
        // Don't bother doing anything if we can't take damage anyway
        if(!invincible)
        {
            // Detecting projectiles
            if (other.gameObject.CompareTag("EnemyProjectile"))
            {
                Projectile projectile = other.gameObject.GetComponent<Projectile>();
                ChangeHealthBy(projectile.damage);
                Destroy(other.gameObject);
            }

            // Collision with enemy body
            else if(other.gameObject.CompareTag("Enemy"))
            {
                ChangeHealthBy(-1);
            }
        }        
    }


    /* Called every frame for which a collision persists.
     */ 
    private void OnCollisionStay(Collision other)
    {
        if(!invincible && other.gameObject.CompareTag("Enemy"))
        {
            ChangeHealthBy(-1);
        }
    }


    /* Temporarily turns the player invincible for invincibilityDurationSeconds seconds.
     */
    private IEnumerator BecomeTemporarilyInvincible()
    {
        Debug.Log("Player became invulnerable to damage!");
        invincible = true;
        float tick = 1 / invincibilityDurationSeconds;

        for(float i = 0; i < invincibilityDurationSeconds; i += tick)
        {
            playerRenderer.enabled = !playerRenderer.enabled;
            yield return new WaitForSeconds(tick);
        }

        Debug.Log("Player is no longer invulnerable!");
        playerRenderer.enabled = true;
        invincible = false;
    }
}
