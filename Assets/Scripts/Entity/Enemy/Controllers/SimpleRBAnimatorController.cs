using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
    This script simply sets the velocity in any children
    animators to that of the rigidbody.
 */
[RequireComponent(typeof(Rigidbody))]
public class SimpleRBAnimatorController : MonoBehaviour, IOnDeathController, IOnPossessionController
{
    Animator ani;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        ani = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (ani == null || rb == null) return;

        ani.SetFloat("velocity", rb.velocity.magnitude);
    }

    public void OnDeath()
    {
        ani?.SetTrigger("dead");
    }

    public void OnPossession()
    {
        // Check for an animator now
        ani = GetComponentInChildren<Animator>();
    }
}
