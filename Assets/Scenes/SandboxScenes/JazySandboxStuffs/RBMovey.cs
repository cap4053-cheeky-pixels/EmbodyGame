using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBMovey : MonoBehaviour
{
    public float offset = 5;
    public float force = 25;
    public float speed = 3;
    private Rigidbody rb;
    private Vector3 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (rb.isKinematic)
        {
            float moveX = initialPosition.x + Mathf.PingPong(Time.time * speed, offset*2) - offset;
            transform.position = new Vector3(moveX, transform.position.y, transform.position.z);
        }
        else
        {
            if ((force > 0 && transform.position.x - initialPosition.x > offset)
                || (force < 0 && transform.position.x - initialPosition.x < -offset))
            {
                force = -force;
            }

            rb.AddForce(force, 0, 0);
        }
    }
}
