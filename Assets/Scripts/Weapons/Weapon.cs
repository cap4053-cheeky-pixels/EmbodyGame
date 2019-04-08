﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    // The amount of damage this weapon deals
    public int damage;

    // The number of seconds between attacks
    public float timeBetweenAttacks;

    // Used for spreading out attacks in timeBetweenAttacks intervals
    protected float timer;


    /* Initializes the timer.
     */  
    protected void Awake()
    {
        // Initially, you should be able to attack immediately
        timer = timeBetweenAttacks + 1;
    }


    /* Accumulates the timer on each frame.
     */
    private void Update()
    {
        timer += Time.deltaTime;
    }


    // All weapons must implement this method
    public abstract void Attack(string tag, GameObject target = null);
}