using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    private Vector3 bufferedForce; // Store force to apply on next fixedupdate

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 direction)
    {
        transform.forward = direction;
        bufferedForce = direction * speed;
    }

    void FixedUpdate()
    {
        rb.AddForce(bufferedForce);
    }
}
