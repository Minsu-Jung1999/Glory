using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainCharacterMovement : MonoBehaviour
{
    #region 공개 프로퍼티들
    [SerializeField]
    float lockOnSpeed = 0.0f;
    [SerializeField]
    GameObject cameraArm;
    [SerializeField]
    float cameraRotationLimitUP = 90.0f;
    [SerializeField]
    float cameraRotationLimitDown = 90.0f;
    [SerializeField]
    float lockOnDistance = 0.0f;
    [SerializeField]
    GameObject weaponManager;
    [SerializeField]
    GameObject aimUI;
    public bool isArmed = false;   // 무기 장착 여부

    #endregion 

    #region 비공개 프로퍼티들

    private Rigidbody rb;
    private MainCharacterAnimatorControll animcontroller;
    private MC_Interaction mc_interaction;
    private GameObject target;
    private StatusManager status;
    private float xRotation = 0;
    private float yRotation = 0;
    private bool isAttack = false;  // 공격 애니매이션 재생
    private bool isLockOn = false;  // 락온 여부
    #endregion


    void Start()
    {
        #region 프로퍼티 초기화
        rb = GetComponent<Rigidbody>();
        animcontroller = GetComponent<MainCharacterAnimatorControll>();
        mc_interaction = GetComponent<MC_Interaction>();
        status = GetComponent<StatusManager>();
        #endregion


        target = null;
    }

    private void FixedUpdate()
    {
        if(!mc_interaction.IsInventoryOpen() && !GameManager.instance.isCommunicating)
        {
            Move();
            LookAround();   // FixedUpdate에서 작동해야 끊기지 않고 카메라가 이동한다.
        }
    }

    void Update()
    {
        if(!mc_interaction.isAnywindowOpen)
        {
            if (UnityEngine.Input.GetMouseButtonDown(0) && isArmed) // 공격
            {
                animcontroller.OnAttack();
                isAttack = true;
            }
            if (UnityEngine.Input.GetMouseButton(1) && isArmed) // 락온 온
            {
                animcontroller.LockOn();
                isLockOn = true;
            }
            else if (UnityEngine.Input.GetMouseButtonUp(1)) // 락온 오프
            {
                animcontroller.LockOff();
                isLockOn = false;
                target = null;
            }
        }
    }

    private void Move()
    {
        Vector2 moveInput = new Vector2(UnityEngine.Input.GetAxisRaw("Horizontal"), UnityEngine.Input.GetAxisRaw("Vertical"));
        bool isMove = moveInput.magnitude != 0;
        animcontroller.CharacterMove(moveInput);

        if (isMove && !isAttack)
        {

            Vector3 moveDir = CalculateMoveDirection(moveInput);
            Rotate(moveDir);
            
            rb.velocity = moveDir * (
                (UnityEngine.Input.GetKey(KeyCode.LeftShift) && !isLockOn)              // 락온 상태에서 달리기 버튼 누르면 
                ? status.GetCurrentRunSpeed()                                              // true = 달리기                 
                : status.GetCurrentWalkSpeed());                                           // false = 걷기
        }
        else   // Idle 상태
        {
            rb.velocity = Vector3.zero;
        }
    }

    private Vector3 CalculateMoveDirection(Vector2 moveInput)
    {
        Vector3 lookForward = new Vector3(cameraArm.transform.forward.x, 0f, cameraArm.transform.forward.z).normalized;
        Vector3 lookRight = new Vector3(cameraArm.transform.right.x, 0f, cameraArm.transform.right.z).normalized;
        Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;
        return moveDir.normalized;
    }

    private void Rotate(Vector3 moveDirection)
    {
        if(target ==null)
        {
            // 원하는 회전 각도를 계산합니다.
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            // 부드러운 회전을 위해 Slerp를 사용합니다.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, status.GetCurrentRotateSpeed()* Time.deltaTime);
        }
    }

    public void SetTarget(GameObject _target)
    {
        if(isLockOn && target==null)
            target = _target;
    }

    private void LookAround()
    {
        if (isLockOn && target != null)
        {
            // 타겟 방향을 바라보도록 회전
            Vector3 targetDirection = target.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            rb.MoveRotation(targetRotation);
            // CameraArm 이 타겟 방향을 바라보도록
            cameraArm.transform.rotation = Quaternion.Euler(targetRotation.eulerAngles.x, targetRotation.eulerAngles.y, 0f);
            aimUI.SetActive(false);
        }
        else
        {
            xRotation += UnityEngine.Input.GetAxis("Mouse X");
            yRotation -= UnityEngine.Input.GetAxis("Mouse Y");
            yRotation = Mathf.Clamp(yRotation, -cameraRotationLimitDown, cameraRotationLimitUP);

            cameraArm.transform.rotation = Quaternion.Euler(yRotation, xRotation, 0);
            aimUI.SetActive(true);
        }
    }

    public void AttackEnd()
    {
        isAttack = false;
    }
}
