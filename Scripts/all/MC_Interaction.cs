using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MC_Interaction : MonoBehaviour
{
    [SerializeField]
    Image inventory;
    [SerializeField]
    GameObject weaponSlot;

    ItemManager itemManager;

    private bool isInventoryOpen = false;

    private void Start()
    {
        itemManager = GetComponentInChildren<ItemManager>();
    }
    private void Update()
    {
        Cursor.visible = inventory.gameObject.activeSelf;
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OpenInventory();
        }

        if(Input.GetMouseButton(0))
        {

        }
    }

    private void OpenInventory()
    {
        if (inventory.gameObject.activeSelf)
        {
            inventory.gameObject.SetActive(false); // �κ��丮�� ��Ȱ��ȭ
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            inventory.gameObject.SetActive(true); // �κ��丮�� Ȱ��ȭ
            Cursor.lockState = CursorLockMode.None;
        }
        

    }

}
