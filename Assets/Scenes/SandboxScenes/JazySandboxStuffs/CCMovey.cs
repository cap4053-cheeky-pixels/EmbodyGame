using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCMovey : MonoBehaviour
{
    public float offset = 5;
    public float speed = 5;
    public bool detectCollisions = true; // false fixes double trigger
    private Vector3 initialPosition;
    private CharacterController cc;
    private Vector3 move;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        cc.detectCollisions = detectCollisions;
        initialPosition = transform.position;
        move = new Vector3(speed, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if ((move.x > 0 && transform.position.x - initialPosition.x > offset)
            || (move.x < 0 && transform.position.x - initialPosition.x < -offset))
        {
            move.x = -move.x;
        }
        cc.SimpleMove(move);
    }
}
