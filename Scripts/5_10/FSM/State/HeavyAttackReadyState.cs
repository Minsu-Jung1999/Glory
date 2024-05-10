using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttackReadyState : State
{

    private float waitTime;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        waitTime = Random.Range(0.5f, 1.5f);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // ��� �ð��� ������ Heavy Attack ���� ��ȯ
        waitTime -= Time.deltaTime;
        if (waitTime <= 0)
        {
            animator.SetTrigger("HeavyAttack");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
