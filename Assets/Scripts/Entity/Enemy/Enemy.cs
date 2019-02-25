﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    // Used to signal an enemy's death to the rooms that spawned them
    public delegate void Died(GameObject who);
    public event Died deathEvent;

    //Can this entity be Possessed 
    public bool isPossessable = false;

    /* Called before the game starts. Sets up all necessary info.
     */
    void Awake()
    {
        isPossessable = true;
        SetWeapon(weapon);
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
        Health += amount;

        if(Health <= 0)
        {
            // TODO add logic for possession and heart drops

            // Signal the death of this enemy
            deathEvent?.Invoke(gameObject);
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
            ChangeHealthBy(projectile.damage);
            Destroy(other.gameObject);
        }
    }
}
