using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MC_Interaction : MonoBehaviour
{
    [SerializeField]
    GameObject inventory;
    [SerializeField]
    GameObject menu;
    [SerializeField]
    GameObject setting;
    [SerializeField]
    GameObject weaponSlot;
    [SerializeField]
    GameObject questLog;

    [SerializeField]
    GameObject windows;

    public bool isAnywindowOpen;

    // �ڽ� ������Ʈ �߿��� SetActive(true)�� ������Ʈ�� �ִ��� ���θ� Ȯ���� �ο� ����
    private bool isActiveChild = false;

    private GameObject communicateNPC;

    private GameObject openingObject;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState= CursorLockMode.Locked;
        // windows ������Ʈ�� ��� �ڽ� ������Ʈ ��������
    }

    private void Update()
    {
        MouseVisibleController();
        // Open �Ǿ� �ִ� UI â�� �˻��ϴ� �ݺ���
        foreach (Transform child in windows.transform)
        {
            if (child.gameObject.activeSelf)
            {
                openingObject = child.gameObject;
                isAnywindowOpen = true;
                break;
            }
            else
            {
                isAnywindowOpen = false;
            }
        }
        HandleInputs();

    }

    private void HandleInputs()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OpenAndCloseInventory();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isAnywindowOpen)
            {
                openingObject.SetActive(false);
            }
            else
            {
                menu.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameManager.instance.questState(true);
            if (communicateNPC != null)
                QuestableObject();

        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            questLog.SetActive(!questLog.activeSelf);
        }
    }

    private void MouseVisibleController()
    {
        // ��� �ڽ� ������Ʈ�� Ȯ���Ͽ� SetActive(true)���� ���� Ȯ��
        foreach (Transform child in windows.transform)
        {
            if (child.gameObject.activeSelf)
            {
                isActiveChild = true;
                Cursor.visible = true;
                Cursor.lockState= CursorLockMode.None;
                break; // �̹� SetActive(true)�� �ڽ� ������Ʈ�� �߰ߵǾ����Ƿ� ���� ����
            }
        }
        if(!isActiveChild)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        // isActiveChild ���� �ʱ�ȭ
        isActiveChild = false;
    }

    private void OpenAndCloseInventory()
    {
        if (inventory.gameObject.activeSelf)
        {
            inventory.gameObject.SetActive(false); // �κ��丮�� ��Ȱ��ȭ
        }
        else
        {
            inventory.gameObject.SetActive(true); // �κ��丮�� Ȱ��ȭ
        }
    }

    public bool IsInventoryOpen()
    {
        return inventory.gameObject.activeSelf;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetCommuicateObject(GameObject gameObject)
    {
        communicateNPC = gameObject;
    }

    public void QuestableObject()
    {
        QuestObject obj = communicateNPC.GetComponent<QuestObject>();
        if(obj != null)
        {
            obj.CommunicateQuest();
        }
    }

}
