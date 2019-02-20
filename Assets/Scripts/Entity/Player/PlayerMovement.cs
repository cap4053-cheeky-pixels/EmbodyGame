using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private CharacterController playerCC;

    void Awake()
    {
        playerCC = GetComponent<CharacterController>();
    }

    void Update()
    {
        float hori = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        Vector3 move = (transform.forward * vert) + (transform.right * hori);
        move *= speed;

        playerCC.SimpleMove(move);
    }
}
