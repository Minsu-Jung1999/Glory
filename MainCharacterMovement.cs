using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Windows;
using UnityEngine;
using Unity.VisualScripting;
using TMPro;


public class MainCharacterMovement : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 0.0f;
    [SerializeField]
    float runSpeed = 0.0f;
    [SerializeField]
    float lookRotationSpeed = 10.0f;
    [SerializeField]
    float moveRotationSpeed = 10.0f;


    [SerializeField]
    float onLockOnSpeed = 0.0f;
    [SerializeField]
    GameObject cameraArm;
    [SerializeField]
    float cameraRotationLimitUP = 90.0f;
    [SerializeField]
    float cameraRotationLimitDown = 90.0f;
    [SerializeField]
    GameObject weaponManager;
    private Vector2 speedPercent;
    [SerializeField]
    float lockOnDistance = 0.0f;
    public bool isArmed = false;   // ���� ���� ����

    private float xRotation = 0;
    private float yRotation = 0;
    private float currentSpeed = 0;
    private bool isAttack = false;  // ���� �ִϸ��̼� ���
    [SerializeField]
    private bool isLockOn = false;  // ���� ����

    private Rigidbody rb;
    private MainCharacterAnimatorControll animcontroller;
    private GameObject target;
    private Camera maincam;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animcontroller = GetComponent<MainCharacterAnimatorControll>();
        maincam = GetComponentInChildren<Camera>();
        target = null;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Move();
        LookAround();
    }

    private void Move()
    {
        Vector2 moveInput = new Vector2(UnityEngine.Input.GetAxisRaw("Horizontal"), UnityEngine.Input.GetAxisRaw("Vertical"));
        bool isMove = moveInput.magnitude != 0;
        animcontroller.CharacterMove(moveInput);
        float desiredMoveSpeed = Mathf.Min(moveInput.magnitude, 0.5f);

        if (isMove && !isAttack)
        {
            currentSpeed = moveSpeed;
            // �޸���
            if (UnityEngine.Input.GetKey(KeyCode.LeftShift) && !isLockOn)
            {
                currentSpeed = runSpeed;
                animcontroller.OnRun();
            }
            Vector3 lookForward = new Vector3(cameraArm.transform.forward.x, 0f, cameraArm.transform.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.transform.right.x, 0f, cameraArm.transform.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;
            moveDir = moveDir.normalized; // ���� ����ȭ(�밢�� �̵��� ���� �ӵ� ����)
           
            Rotate(moveDir);
            transform.position += moveDir * Time.deltaTime * currentSpeed;

        }
        else
        {
            currentSpeed = 0;
        }

    }
    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }

    private void Rotate(Vector3 moveDirection)
    {
        if(target ==null)
        {
            // ���ϴ� ȸ�� ������ ����մϴ�.
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            // �ε巯�� ȸ���� ���� Slerp�� ����մϴ�.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, moveRotationSpeed * Time.deltaTime);
        }
    }

    void Update()
    {
        
        if (UnityEngine.Input.GetMouseButtonDown(0) && isArmed)
        {
            animcontroller.OnAttack();
            isAttack = true;
        }
        if(UnityEngine.Input.GetMouseButton(1) && isArmed)
        {
            LockOnRay();
            animcontroller.LockOn();
            isLockOn = true;
        }
        else if (UnityEngine.Input.GetMouseButtonUp(1))
        {
            animcontroller.LockOff();
            isLockOn = false;
            target = null;
        }
    }

    private void LockOnRay()
    {
        if (maincam != null && target==null)
        {
            // ī�޶� �ٶ󺸴� ������ ���ϱ� ���� ȭ���� �߽����� �������� �� ����ĳ��Ʈ
            Ray cameraCenterRay = maincam.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
            float sphereScale = Mathf.Max(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);
            if (Physics.SphereCast(maincam.transform.position, sphereScale / 2.0f, cameraCenterRay.direction, out RaycastHit hit, lockOnDistance))
            {
                if (hit.transform.tag == "Enemy")
                {
                    currentSpeed = onLockOnSpeed;
                    target = hit.transform.gameObject;
                }
            }
        }
        else
        {
            print("Camera can not be found");
        }
    }

    private void LookAround()
    {
        if (isLockOn && target != null)
        {
            // Ÿ�ٰ� ī�޶� �� ���� ���� ���� ���
            Vector3 direction = target.transform.position - cameraArm.transform.position;
            direction.y = 0f; // ���� ������ y ������ 0���� ����� ���� ������ ������ ����

            // ī�޶� ���� ������ Ÿ���� ���ϵ��� ����
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                cameraArm.transform.rotation = Quaternion.Lerp(cameraArm.transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);
            }

            // Ÿ�ٰ� ��� ���� ���� ���� ���
            direction = target.transform.position - gameObject.transform.position;
            direction.y = 0f; // ���� ������ y ������ 0���� ����� ���� ������ ������ ����

            // ����� ������ Ÿ���� ���ϵ��� ����
            if (direction != Vector3.zero)
            {

                Quaternion lookRotation = Quaternion.LookRotation(direction);
                lookRotation *= Quaternion.Euler(0, -20, 0); // y������ -20��ŭ ȸ��
                rb.MoveRotation(lookRotation);
            }
        }
        else
        {
            xRotation += UnityEngine.Input.GetAxis("Mouse X");
            yRotation -= UnityEngine.Input.GetAxis("Mouse Y");
            yRotation = Mathf.Clamp(yRotation, -cameraRotationLimitDown, cameraRotationLimitUP);

            cameraArm.transform.rotation = Quaternion.Euler(yRotation, xRotation, 0);
        }
    }

    public void AttackEnd()
    {
        isAttack = false;
        print("MC_End");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Weapon")
        {
            animcontroller.GetHit();
        }
    }

}
