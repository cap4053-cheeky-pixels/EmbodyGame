using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;


public class DevilAnimationTest : MonoBehaviour
{
    Animator animator;
    Enemy devil;

    private void Awake()
    {
        animator = gameObject.GetComponent<Enemy>().model.GetComponent<Animator>();
        devil = gameObject.GetComponent<Enemy>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("Fly");
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            animator.SetTrigger("AttackMelee");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetTrigger("AttackRanged");
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            devil.Health -= 1;
            animator.SetTrigger("RecoilFromDamage");
        }

        if(Input.GetKeyDown(KeyCode.N))
        {
            animator.SetTrigger("BecomeStunned");
        }

        if(Input.GetKeyDown(KeyCode.V))
        {
            animator.SetTrigger("VictoryPose");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("DeathPose");
        }
    }
}
