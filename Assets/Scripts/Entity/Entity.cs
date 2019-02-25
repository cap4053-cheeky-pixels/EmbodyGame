﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    // This entity's current health (at most MaxHealth)
    public int Health;

    // The maximum health this entity can currently have
    public int MaxHealth;

    // The model that's expected to be a child of this game object
    public GameObject model;
    public void SetModel(GameObject model) { this.model = model; }

    // Changes this entity's maximum health by the given amount
    public abstract void ChangeMaxHealthBy(int amount);

    // Changes this entity's current health by the given amount
    public abstract void ChangeHealthBy(int amount);
}
