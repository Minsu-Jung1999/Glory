using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class State : StateMachineBehaviour
{
    protected enum AttackState
    {
        Guard,
        Dodge,
        Attack,
        HeavyAttack,
        Max 
    }

    protected Transform boss_transform;
    protected Transform player_transform;
    protected StatusManager statusManager;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss_transform = animator.GetComponent<Transform>();
        player_transform = GameObject.Find("Player").GetComponent<Transform>();
        statusManager = animator.GetComponent<StatusManager>();
        Debug.Log("Distance : " + Vector3.Distance(boss_transform.position , player_transform.position));
    } 

    protected float GetDistance()
    {
        return Vector3.Distance(boss_transform.position, player_transform.position);
    }

    protected void MoveToTarget(float speed)
    {
        // Calculate direction to the player
        Vector3 direction = (player_transform.position - boss_transform.position).normalized;

        // Rotate boss to face the player
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        boss_transform.rotation = Quaternion.Slerp(boss_transform.rotation, lookRotation, Time.deltaTime * statusManager.GetCurrentRotateSpeed());

        // Set velocity to move towards the player
        Rigidbody bossRigidbody = boss_transform.GetComponent<Rigidbody>();
        bossRigidbody.velocity = direction * speed;
    }
}
