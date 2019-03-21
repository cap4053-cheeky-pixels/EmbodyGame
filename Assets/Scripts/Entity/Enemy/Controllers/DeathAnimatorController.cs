using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimatorController : MonoBehaviour, IOnDeathController
{
    Animator ani;

    void Awake()
    {
        ani = GetComponentInChildren<Animator>();
    }

    public void OnDeath()
    {
        ani.SetTrigger("dead");
    }
}
