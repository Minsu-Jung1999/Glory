using UnityEngine;

[RequireComponent(typeof(Rigidbody)),RequireComponent(typeof(Animator)),RequireComponent(typeof(StatusManager))]
public class GoblinMovement : MonoBehaviour
{
    public AttackCollision left_attackCollision;
    public AttackCollision right_attackCollision;

    public float attackRange;
    public float minWaitTime = 1f;
    public float maxWaitTime = 3f;
    public float roamRadius;

    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool isDetected = false;
    private bool inAttackRange = false;
    private bool isAttack = false;
    private float waitTime;
    private Rigidbody rigid;
    private Animator anim;
    private StatusManager statusManager;

    void Start()
    {
        statusManager = GetComponent<StatusManager>();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        waitTime = Random.Range(minWaitTime, maxWaitTime);
        SetRandomTargetPosition();
    }

    void Update()
    {
        if(rigid)
            anim.SetFloat("MoveSpeed", rigid.velocity.magnitude, 0.2f,Time.deltaTime);

        if (isMoving && !isAttack)
        {
            MoveTowardsTarget();
        }
        else
        {
            WaitAtTarget();
        }
        
        if (inAttackRange)
        {
            isAttack = true;
            anim.SetBool("IsLockOn", true);
            anim.SetTrigger("OnAttack");
            Vector3 direction = (targetPosition - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            lookRotation *= Quaternion.Euler(0, -20, 0); // y축으로 -20만큼 회전
            rigid.velocity = Vector3.zero;
            rigid.MoveRotation(lookRotation);
            if (isDetected && Vector3.Distance(transform.position, targetPosition) >= attackRange)
            {
                inAttackRange = false;  
            }
        }
    }

    void MoveTowardsTarget()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;

        // 평균 속도
        float currentSpeed = statusManager.GetCurrentWalkSpeed();

        // 플레이어를 탐지했고 공격범위에 플레이어가 있을 경우
        if (isDetected && Vector3.Distance(transform.position, targetPosition) <= attackRange)
        {
            isMoving= false;
            inAttackRange = true;

            return;
        }
        else
        {
            inAttackRange = false;
            anim.SetBool("IsLockOn", false);
        }

        if (!inAttackRange)
        {
            // 플레이어를 감지하여 달려갈 때 속도
            if (isDetected)
                currentSpeed = statusManager.GetCurrentRunSpeed();

            rigid.velocity = direction * currentSpeed;

            // 평상시 패트롤 모드
            if (!isDetected && Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMoving = false;
                SetRandomTargetPosition();
            }
            else
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                rigid.MoveRotation(Quaternion.Lerp(transform.rotation, targetRotation, statusManager.GetCurrentRotateSpeed() * Time.deltaTime));
            }
        }
    }

    void WaitAtTarget()
    {
        waitTime -= Time.deltaTime;

        if (waitTime <= 0)
        {
            isMoving = true;
            waitTime = Random.Range(minWaitTime, maxWaitTime);
        }
    }

    void SetRandomTargetPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
        randomDirection += transform.position;
        randomDirection.y = transform.position.y;
        targetPosition = randomDirection;
    }

    public void AttackEnd()
    {
        isAttack = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Weapon")
        {
            anim.SetTrigger("GetHit");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isDetected = true;
            targetPosition = other.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isDetected = false;
            targetPosition = transform.position;
        }
    }

    public void AttackCollisionOn()
    {
        left_attackCollision.AttackCollisionOn();
        right_attackCollision.AttackCollisionOn();
    }

    public void AttackCollisionOff()
    {
        left_attackCollision.AttackCollisionOff();
        right_attackCollision.AttackCollisionOff();

    }

    public void OnDeath()
    {
        anim.SetTrigger("Death");
        anim.SetBool("isDeath", true);
        rigid.useGravity = false;
        GetComponent<CapsuleCollider>().enabled = false;
    }
    
    public void DestroyObject()
    {
        Destroy(gameObject,2f);
    }
}
