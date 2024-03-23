using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;
using System;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
#region
    // TEST
    [SerializeField]
    ItemData onehandSowrd;
    [SerializeField]
    ItemData dualhandSowrd;
    #endregion
    [SerializeField]
    GameObject itemManager;

    [SerializeField]
    GameObject weaponEquipmentSlot;

    [SerializeField]
    private int maxItemSlots = 12;

    [SerializeField]
    private GameObject itemSlotsContent;

    [SerializeField]
    private ItemData[] inventoryItems;

    [SerializeField]
    private GameObject[] inventorySlots;

    private int currentItemSlotIndex = 0;

    private void Start()
    {
        inventoryItems = new ItemData[maxItemSlots];
        inventorySlots = new GameObject[maxItemSlots];
        AddItemSlotsFromContent();
        //TEST
        AddItemInInventory(onehandSowrd);
        AddItemInInventory(dualhandSowrd);
        AddItemInInventory(onehandSowrd);
    }

    private void AddItemSlotsFromContent()
    {
        if (itemSlotsContent == null)
        {
            Debug.LogError("Item slots content is not assigned!");
            return;
        }
        Transform[] childTransforms = itemSlotsContent.GetComponentsInChildren<Transform>(true);
        int count = 0;
        // itemSlotsContent�� �ڽ� ��ü�� ������ �迭�� �߰�
        foreach (Transform childTransform in childTransforms)
        {
            if (childTransform.parent.name == "Content" && childTransform.gameObject != itemSlotsContent)
            {
                inventorySlots[count++] = childTransform.GetChild(0).gameObject;
            }
        }
    }

    // �κ��丮�� ������ �߰�
    public void AddItemInInventory(ItemData item)
    {
        GameObject Weapon = item._itemOb;

        if (currentItemSlotIndex < maxItemSlots)
        {
            inventoryItems[currentItemSlotIndex] = item;
            GameObject itemSlotObject = inventorySlots[currentItemSlotIndex];
            Image imageComponent = itemSlotObject.GetComponent<Image>();

            // �̹��� ������Ʈ�� ���� ��쿡�� �߰��ϰ� Ȱ��ȭ�մϴ�.
            if (imageComponent == null)
            {
                // Image ������Ʈ �߰�
                imageComponent = itemSlotObject.AddComponent<Image>();
                // �̹��� ��������Ʈ ����
                imageComponent.sprite = item._icon;
            }

            currentItemSlotIndex++;
        }
        else
        {
            print("�κ��丮�� �ڸ��� �����ϴ�.");
        }

    }

    // ������ ������ Ŭ���� �̺�Ʈ
    public void OnItemClick()
    {
        // ���콺�� Ŭ���� UI ��� ��������
        GameObject clickedObject = EventSystem.current.currentSelectedGameObject;

        if (clickedObject != null)
        {
            // UI ����� �ڽ� ��ü�� �߿��� Image ������Ʈ�� ã��
            Image imageComponent = clickedObject.transform.GetChild(0).GetComponent<Image>();
            imageComponent.color = Color.white;
            if (imageComponent != null)
            {
                WeaponEquipment(clickedObject, imageComponent);
            }
        }
        else
        {
            Debug.Log("���콺�� Ŭ���� UI ��Ұ� �����ϴ�.");
        }
    }
    
    // �κ��丮 ������ ĳ���� ����â���� �̵� �� ����
    private void WeaponEquipment(GameObject clickedObject, Image imageComponent)
    {
        int clickedIndexNum = ExtractNumber(clickedObject.GetComponentInParent<Transform>().gameObject.name) - 1;
        if (clickedIndexNum != -1)
        {
            weaponEquipmentSlot.transform.GetChild(0).GetComponentInChildren<Image>().sprite = imageComponent.sprite;
            itemManager.GetComponent<ItemManager>().EquipWeapon(inventoryItems[clickedIndexNum]);
            inventoryItems[clickedIndexNum] = null;
            GameObject itemSlotObject = inventorySlots[clickedIndexNum];
            Destroy(itemSlotObject.GetComponent<Image>());
        }
    }

    int ExtractNumber(string input)
    {
        string pattern = @"\d+"; // ���Խ� ����: ���ӵ� ����
        Match match = Regex.Match(input, pattern); // ���Խ� ���ϰ� ��ġ�ϴ� �κ� ã��

        if (match.Success) // ��ġ�ϴ� �κ��� �ִ� ���
        {
            return int.Parse(match.Value); // ��ġ�ϴ� �κ��� ������ ��ȯ�Ͽ� ��ȯ
        }
        else
        {
            Debug.LogWarning("No number found in the input string.");
            return -1; // ��ġ�ϴ� �κ��� ���� ��� -1�� ��ȯ�ϰų� ������ ���� ó���� ������ �� ����
        }
    }

}
