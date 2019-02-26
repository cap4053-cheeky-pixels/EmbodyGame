using UnityEngine;


public class FlyBehavior : StateMachineBehaviour
{
    private DevilBoss boss;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Yuck, I know
        boss = animator.gameObject.transform.parent.gameObject.GetComponent<DevilBoss>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.FlyTowardsPlayer();
    }
}
