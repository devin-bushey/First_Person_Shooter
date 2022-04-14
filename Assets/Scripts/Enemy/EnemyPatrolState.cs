using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : StateMachineBehaviour
{

    private Enemy enemy;
    private float timer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.gameObject.GetComponentInParent<Enemy>();
        timer = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.Agent.SetDestination(enemy.GetCurrentWaypoint());
        if (enemy.Agent.remainingDistance <= enemy.Agent.stoppingDistance)
        {
            enemy.DetermineNextWaypoint();
            enemy.Agent.SetDestination(enemy.GetCurrentWaypoint());
        }


        else if (enemy.GetDistanceFromPlayer() < enemy.ChaseRange)
        {
            animator.SetBool("isChasing", true);
        }

        timer += Time.deltaTime;
        if (timer > enemy.PatrolTime)
        {
            animator.SetBool("isPatrolling", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
