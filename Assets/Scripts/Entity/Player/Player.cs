using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    // Used to signal health change events to the heart container system
    public delegate void HealthChanged();
    public event HealthChanged healthChangedEvent;
    public delegate void GoldenHeathChanged();
    public event GoldenHeathChanged goldenHealthEvent;

    // Used to signal player's death
    public delegate void Died();
    public event Died deathEvent;

    // All of this is mainly used for pausing the game
    private bool actionsEnabled;
    public void SetEnabled(bool enabled) { actionsEnabled = enabled; }
    public bool ActionsEnabled() { return actionsEnabled; }

    // All of this is used for invincibility frames
    public float invincibilityDurationSeconds;
    public float delayBetweenInvincibilityFlashes;
    private bool invincible = false;

    public int GoldenHealth;
    public int MaxGoldenHealth = 10;


    /* Called before the game starts. Sets up all necessary info.
     */
    void Awake()
    {
        SetEnabled(true);
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
        // If we're gonna take damage but we're invincible, just short-circuit/fail fast
        if (invincible && amount < 0) return;

        if(amount < 0 && GoldenHealth != 0)
        {
            ChangeGoldenHealthBy(amount);
        }
        else
        {
            Health += amount;
            if (Health < 0) Health = 0;
            healthChangedEvent?.Invoke();
        }

        // Start the invincibility coroutine if we take damage
        if (amount < 0)
        {
            if(damageAudio != null)
            {
                damageAudio.Play();
            }
            StartCoroutine(BecomeTemporarilyInvincible());
        }

        // Player died
        if (Health <= 0)
        {
            if(deathAudio != null)
            {
                deathAudio.Play();
            }
            deathEvent?.Invoke();
        }
    }

    public void ChangeGoldenHealthBy(int amount)
    {
        GoldenHealth += amount;
        if (GoldenHealth < 0)
        {
            Health += GoldenHealth;
            healthChangedEvent?.Invoke();
            GoldenHealth = 0;
        }
        goldenHealthEvent?.Invoke();
    }


    /* Called when this Player encounters another object.
     */
    private void OnCollisionEnter(Collision other)
    {
        // Don't bother doing anything if we can't take damage anyway
        if (invincible) return;

        // Collision with enemies and traps will deal a constant half a heart of damage
        if (other.gameObject.CompareTag("Boss") ||
            other.gameObject.CompareTag("Trap"))
        {
            ChangeHealthBy(-1);
        }
    }


    /* Temporarily turns the player invincible for invincibilityDurationSeconds seconds.
     */
    private IEnumerator BecomeTemporarilyInvincible()
    {
        //Debug.Log("Player turned invincible!");
        invincible = true;

        // Flash on and off for roughly invincibilityDurationSeconds seconds
        for(float i = 0; i < invincibilityDurationSeconds; i += delayBetweenInvincibilityFlashes)
        {
            // Alternate between 0 and 1 scale to simulate flashing without actually disabling anything
            if (model.transform.localScale == Vector3.one) model.transform.localScale = Vector3.zero;
            else model.transform.localScale = Vector3.one;

            // Wait for this many seconds before flashing again
            yield return new WaitForSeconds(delayBetweenInvincibilityFlashes);
        }

        //Debug.Log("Player no longer invincible!");
        model.transform.localScale = Vector3.one;
        invincible = false;
    }
}
