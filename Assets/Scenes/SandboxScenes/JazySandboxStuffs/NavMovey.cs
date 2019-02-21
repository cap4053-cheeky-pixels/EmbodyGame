using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMovey : MonoBehaviour
{
    public float offset = 5;
    private Vector3 initialPosition;
    private NavMeshAgent nma;
    private Vector3 move;
    // Start is called before the first frame update
    void Awake()
    {
        nma = GetComponent<NavMeshAgent>();
        initialPosition = transform.position;
        move = new Vector3(offset, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if ((move.x > 0 && transform.position.x - initialPosition.x > offset)
            || (move.x < 0 && transform.position.x - initialPosition.x < -offset))
        {
            move.x = -move.x;
        }
        nma.SetDestination(transform.position + move);
    }
}
