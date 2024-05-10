using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : State
{
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        MoveToTarget(statusManager.GetCurrentRunSpeed());
        // ���� ���� ���۵Ǹ� �÷��̾ ���� �ɾ��.
        // ������ �÷��̾��� �Ÿ��� 10���� �۰ų� ������ �̵� ����
        if (GetDistance() <= 6f)
        {
            animator.SetTrigger("Walk");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
