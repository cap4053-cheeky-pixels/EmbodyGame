using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject target;

    // Awake is called before the game starts
    void Awake()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Designates the target for this movement script
    public void SetTargetTo(GameObject target)
    {
        this.target = target;
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            agent.SetDestination(target.transform.position);
        }
    }

    

}
