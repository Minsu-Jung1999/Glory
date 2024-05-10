using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : State
{
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        MoveToTarget(statusManager.GetCurrentRunSpeed());
        // 보스 전이 시작되면 플레이어를 향해 걸어간다.
        // 보스와 플레이어의 거리가 10보다 작거나 같으면 이동 시작
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
