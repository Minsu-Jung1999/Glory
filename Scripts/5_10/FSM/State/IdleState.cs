using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class IdleState : State
{

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // ���� ���� ���۵Ǹ� �÷��̾ ���� �ɾ��.

        // ������ �÷��̾��� �Ÿ��� 5���� �۰ų� ������ �̵� ����
        if (GetDistance() <= 10f)
        {
            animator.SetTrigger("Walk");
        }
        else if(GetDistance() <= 20f)
        {
            animator.SetTrigger("Run");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


}
