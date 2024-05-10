using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterAnimatorControll : MonoBehaviour
{
    [SerializeField]
    ItemManager itemManager;

    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        animator.SetFloat("MoveSpeed", GetComponent<Rigidbody>().velocity.magnitude, 0.02f, Time.deltaTime);
    }

    public void LockOn()
    {
        animator.SetBool("isLockOn", true);
    }

    public void LockOff()
    {
        animator.SetBool("isLockOn", false);
    }

    public void OnAttack()
    {
        animator.SetTrigger("OnAttack");
    }

    public void OnRun()
    {
        animator.SetFloat("Horizontal", 2f);
        animator.SetFloat("Vertical", 2f);
    }

    public void OnWalk()
    {
        animator.speed = 1;
    }

    public void CharacterMove(Vector2 moveInput)
    {
        animator.SetFloat("Horizontal", moveInput.x);
        animator.SetFloat("Vertical", moveInput.y);
    }

    public void ArmedWeapon(int state)
    {
        animator.SetInteger("WeaponID", state);
    }

    public void GetHit()
    {
        animator.SetTrigger("GetHit");
    }

    public void AttackCollisionOn()
    {
        if(itemManager.rightAttackCollision != null)
        {
            itemManager.rightAttackCollision.AttackCollisionOn();
        }
        else
        {
        }
        if(itemManager.leftAttackCollision != null)
        {
            itemManager.leftAttackCollision.AttackCollisionOn();
        }
    }

    public void AttackCollisionOff()
    {
        if (itemManager.rightAttackCollision != null)
        {
            itemManager.rightAttackCollision.AttackCollisionOff();
        }
        if (itemManager.leftAttackCollision != null)
        {
            itemManager.leftAttackCollision.AttackCollisionOff();
        }
    }
}
