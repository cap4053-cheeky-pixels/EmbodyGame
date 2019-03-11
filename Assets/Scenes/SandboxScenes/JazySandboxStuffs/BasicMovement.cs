using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public float speed;
    public Rigidbody playerRB;

    void Start()
    {
        playerRB = (playerRB == null) ? GetComponent<Rigidbody>() : playerRB;
    }

    void FixedUpdate()
    {
        float hori = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        //float inSprint = Input.GetAxis("Sprint");
        float inSprint = Input.GetKey(KeyCode.LeftShift) ? 1 : 0;

        Vector3 move = (transform.forward * vert) + (transform.right * hori);
        move *= speed;

        if (inSprint > 0)
        {
            // If shift is held down then forcefully move the RB
            // (This is intended for Kinematic RBs)
            //playerRB.MovePosition(transform.position + move * Time.deltaTime);
            playerRB.AddForce(move * 10, ForceMode.VelocityChange);
        }
        else
        {
            playerRB.AddForce(move * 10, ForceMode.Force);
            //playerRB.velocity = move; // IGNORES MASS
        }
    }
}
