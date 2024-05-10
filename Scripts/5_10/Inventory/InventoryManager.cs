using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;
using System;


public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    ItemManager itemManager;
    
    // ������ ������ ������
    [SerializeField]
    GameObject[] equipmentSlot;

    // ������ �����ۿ� ���� ����
    [SerializeField]
    ItemData[] equipmentItems;
 
    // �κ��丮 ���� ����
    [SerializeField]
    GameObject[] inventorySlots;

    // �κ��丮�� �ִ� �����ۿ� ���� ����
    [SerializeField]
    List<ItemData> itemData = new List<ItemData>();

    private void Start()
    {
        print("Inventory start");
        // �κ��丮�� �ִ� �����ۿ� ���� ������ �κ��丮 ���� ������ ����
        equipmentItems = new ItemData[equipmentSlot.Length];
    }

    // �κ��丮�� ������ �߰�
    public void AddItem(ItemData item)
    {
        foreach(GameObject slot in inventorySlots)
        {
            Image itemIcon = slot.GetComponent<Image>();
            if (!itemIcon.enabled)
            {
                itemIcon.sprite = item._icon;
                itemIcon.enabled = true;
                itemData.Add(item);
                break;
            }
        }
    }

    // �������� Ŭ���� �κ��丮�� �������� ���� �������� �̵�
    // ���� ������ �迭���� �߰�
    public void EquipItem()
    {
        // ���콺�� Ŭ���� ������������
        GameObject clickedObject = EventSystem.current.currentSelectedGameObject;

        // Ŭ���� ������Ʈ�� �迭 ��ġ ����
        int index = 0;

        // �κ��丮���� ������ ������ ã�� ���� �������� �̵�
        foreach(GameObject slot in inventorySlots)
        {
            // Ŭ���� ������Ʈ�� �κ��丮���� ã��
            if(clickedObject == slot)
            {
                ItemData item = itemData[index];

                // �̹��� ������Ʈ ��Ȱ��ȭ
                slot.GetComponent<Image>().enabled = false; 

                // ���� ���� ���Կ� �������� �ִٸ� ���� �ϰ� �ִ� �������� �κ��丮�� �߰� �Ѵ�
                if(equipmentItems[(int)item.itemType] != null)
                {
                    AddItem(equipmentItems[(int)item.itemType]);
                }

                // Ŭ���� ������Ʈ�� ���� ���Կ� �߰��ϱ�
                equipmentItems[(int)item.itemType] = item;
                itemManager.EquipWeapon(item);
                
                // ���� ���Կ� �̹��� �߰��ϱ�
                equipmentSlot[(int)item.itemType].GetComponent<Image>().sprite = item._icon;

                // �κ��丮�� �ִ� �������� ���� �������� �̵��߱� ������ �κ��丮 ������ �迭���� �ش� �������� ���� �� �ش�.
                itemData.RemoveAt(index);
                break;
            }
            index++;
        }

        // �������� ������ �� �κ��丮 �������� ���� �ǰ� �� �ش�
        SortItem(index);
    }

    // �κ��丮�� ��� ��Ȱ��ȭ �� �� �κ��丮 ������ ����Ʈ�� �������� �������� �ٽ� �����Ѵ�
    private void SortItem(int startIndex)
    {
        // �κ��丮 ������ ��� ��Ȱ��ȭ
        foreach (GameObject slot in inventorySlots)
        {
            slot.GetComponent<Image>().enabled = false;
        }

        // �����۵����� �迭�� ����ؼ� �κ��丮 ������ �����Ѵ�.
        for(int i = 0; i < itemData.Count; i++)
        {
            Image itemIcon = inventorySlots[i].GetComponent<Image>();
            itemIcon.sprite = itemData[i]._icon;
            itemIcon.enabled = true;
        }
    }






}
