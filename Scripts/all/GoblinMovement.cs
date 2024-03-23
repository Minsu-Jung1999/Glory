using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class GoblinMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float runSpeed= 1.0f;
    public float roamRadius = 5f;
    public float minWaitTime = 1f;
    public float maxWaitTime = 3f;
    public float rotationSpeed = 5f;
    public float attackRange = 0;

    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool isDetected = false;
    private bool inAttackRange = false;
    private bool isAttack = false;
    private float waitTime;
    private Rigidbody rigid;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        waitTime = Random.Range(minWaitTime, maxWaitTime);
        SetRandomTargetPosition();
    }

    void Update()
    {
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
            lookRotation *= Quaternion.Euler(0, -20, 0); // y������ -20��ŭ ȸ��

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

        // ��� �ӵ�
        float currentSpeed = moveSpeed;

        // �÷��̾ Ž���߰� ���ݹ����� �÷��̾ ���� ���
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
            // �÷��̾ �����Ͽ� �޷��� �� �ӵ�
            if (isDetected)
                currentSpeed = runSpeed;

            rigid.velocity = direction * currentSpeed;

            // ���� ��Ʈ�� ���
            if (!isDetected && Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMoving = false;
                SetRandomTargetPosition();
            }
            else
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                rigid.MoveRotation(Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime));
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
        print("AttackCollisionOn called");
    }

    public void AttackCollisionOff()
    {
        print("AttackCollisionOff called");
    }
}
