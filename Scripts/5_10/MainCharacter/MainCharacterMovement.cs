using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainCharacterMovement : MonoBehaviour
{
    #region ���� ������Ƽ��
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
    public bool isArmed = false;   // ���� ���� ����

    #endregion 

    #region ����� ������Ƽ��

    private Rigidbody rb;
    private MainCharacterAnimatorControll animcontroller;
    private MC_Interaction mc_interaction;
    private GameObject target;
    private StatusManager status;
    private float xRotation = 0;
    private float yRotation = 0;
    private bool isAttack = false;  // ���� �ִϸ��̼� ���
    private bool isLockOn = false;  // ���� ����
    #endregion


    void Start()
    {
        #region ������Ƽ �ʱ�ȭ
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
            LookAround();   // FixedUpdate���� �۵��ؾ� ������ �ʰ� ī�޶� �̵��Ѵ�.
        }
    }

    void Update()
    {
        if(!mc_interaction.isAnywindowOpen)
        {
            if (UnityEngine.Input.GetMouseButtonDown(0) && isArmed) // ����
            {
                animcontroller.OnAttack();
                isAttack = true;
            }
            if (UnityEngine.Input.GetMouseButton(1) && isArmed) // ���� ��
            {
                animcontroller.LockOn();
                isLockOn = true;
            }
            else if (UnityEngine.Input.GetMouseButtonUp(1)) // ���� ����
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
                (UnityEngine.Input.GetKey(KeyCode.LeftShift) && !isLockOn)              // ���� ���¿��� �޸��� ��ư ������ 
                ? status.GetCurrentRunSpeed()                                              // true = �޸���                 
                : status.GetCurrentWalkSpeed());                                           // false = �ȱ�
        }
        else   // Idle ����
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
            // ���ϴ� ȸ�� ������ ����մϴ�.
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            // �ε巯�� ȸ���� ���� Slerp�� ����մϴ�.
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
            // Ÿ�� ������ �ٶ󺸵��� ȸ��
            Vector3 targetDirection = target.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            rb.MoveRotation(targetRotation);
            // CameraArm �� Ÿ�� ������ �ٶ󺸵���
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
