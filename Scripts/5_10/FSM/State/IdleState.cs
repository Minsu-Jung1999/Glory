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
        // 보스 전이 시작되면 플레이어를 향해 걸어간다.

        // 보스와 플레이어의 거리가 5보다 작거나 같으면 이동 시작
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
