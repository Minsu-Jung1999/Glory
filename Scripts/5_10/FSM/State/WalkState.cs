using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : State
{

    public int randomValue = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       base.OnStateEnter(animator, stateInfo, layerIndex);
        // 현재 시간을 기반으로 시드 값을 설정
        Random.InitState((int)System.DateTime.Now.Ticks);
        randomValue = Random.Range(0, (int)AttackState.Max); 
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        MoveToTarget(statusManager.GetCurrentWalkSpeed());

        // 랜덤 변수에 따라 다음 상태를 정한다.
        if (GetDistance() <= 3f)
        {
            // 가드
            if (randomValue == (int)AttackState.Guard)                        
            {
                Debug.Log("Guarding!");
                animator.SetTrigger("Guard");
            }
            // 닷지 공격
            else if (randomValue == (int)AttackState.Dodge)                   
            {
                Debug.Log("Dodge!");
                animator.SetTrigger("AttackDodge");
            }
            // 기본 공격
            else if (randomValue == (int)AttackState.Attack)                   
            {
                Debug.Log("Attack");
                animator.SetTrigger("Attack");
            }
            // 강 공격
            else
            {
                Debug.Log("Heavy Attack");
                animator.SetTrigger("HeavyAttackReady");
            }           
        }
        // 플레이어가 일정 거리 이상이면 달리기로 변환한다.
        else if(GetDistance() >= 10f)
        {
            animator.SetTrigger("Run");
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
