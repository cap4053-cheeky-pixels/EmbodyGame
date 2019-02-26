using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class FlyBehavior : StateMachineBehaviour
{
    DevilBoss boss;
    NavMeshAgent bossAgent;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.gameObject.transform.parent.gameObject.GetComponent<DevilBoss>();
        bossAgent = boss.GetAgent();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Fly towards the player
        bossAgent.SetDestination(boss.GetPlayer().transform.position);

        Debug.Log("Stopping: " + bossAgent.stoppingDistance + " Remaining: " + bossAgent.remainingDistance);

        // If we arrive at our destination, go back to Idle
        if (!bossAgent.pathPending)
        {
            if (bossAgent.remainingDistance <= bossAgent.stoppingDistance)
            {
                if (!bossAgent.hasPath || bossAgent.velocity.sqrMagnitude == 0f)
                {
                    animator.SetTrigger("StopFlying");
                }
            }
        }
    }
}
