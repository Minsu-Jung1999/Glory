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

    // 자식 오브젝트 중에서 SetActive(true)인 오브젝트가 있는지 여부를 확인할 부울 변수
    private bool isActiveChild = false;

    private GameObject communicateNPC;

    private GameObject openingObject;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState= CursorLockMode.Locked;
        // windows 오브젝트의 모든 자식 오브젝트 가져오기
    }

    private void Update()
    {
        MouseVisibleController();
        // Open 되어 있는 UI 창을 검색하는 반복문
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
        // 모든 자식 오브젝트를 확인하여 SetActive(true)인지 여부 확인
        foreach (Transform child in windows.transform)
        {
            if (child.gameObject.activeSelf)
            {
                isActiveChild = true;
                Cursor.visible = true;
                Cursor.lockState= CursorLockMode.None;
                break; // 이미 SetActive(true)인 자식 오브젝트가 발견되었으므로 루프 종료
            }
        }
        if(!isActiveChild)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        // isActiveChild 변수 초기화
        isActiveChild = false;
    }

    private void OpenAndCloseInventory()
    {
        if (inventory.gameObject.activeSelf)
        {
            inventory.gameObject.SetActive(false); // 인벤토리를 비활성화
        }
        else
        {
            inventory.gameObject.SetActive(true); // 인벤토리를 활성화
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
