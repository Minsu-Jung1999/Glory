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
        // ���� �ð��� ������� �õ� ���� ����
        Random.InitState((int)System.DateTime.Now.Ticks);
        randomValue = Random.Range(0, (int)AttackState.Max); 
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        MoveToTarget(statusManager.GetCurrentWalkSpeed());

        // ���� ������ ���� ���� ���¸� ���Ѵ�.
        if (GetDistance() <= 3f)
        {
            // ����
            if (randomValue == (int)AttackState.Guard)                        
            {
                Debug.Log("Guarding!");
                animator.SetTrigger("Guard");
            }
            // ���� ����
            else if (randomValue == (int)AttackState.Dodge)                   
            {
                Debug.Log("Dodge!");
                animator.SetTrigger("AttackDodge");
            }
            // �⺻ ����
            else if (randomValue == (int)AttackState.Attack)                   
            {
                Debug.Log("Attack");
                animator.SetTrigger("Attack");
            }
            // �� ����
            else
            {
                Debug.Log("Heavy Attack");
                animator.SetTrigger("HeavyAttackReady");
            }           
        }
        // �÷��̾ ���� �Ÿ� �̻��̸� �޸���� ��ȯ�Ѵ�.
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
