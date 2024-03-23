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
        // itemSlotsContent의 자식 객체를 가져와 배열에 추가
        foreach (Transform childTransform in childTransforms)
        {
            if (childTransform.parent.name == "Content" && childTransform.gameObject != itemSlotsContent)
            {
                inventorySlots[count++] = childTransform.GetChild(0).gameObject;
            }
        }
    }

    // 인벤토리에 아이템 추가
    public void AddItemInInventory(ItemData item)
    {
        GameObject Weapon = item._itemOb;

        if (currentItemSlotIndex < maxItemSlots)
        {
            inventoryItems[currentItemSlotIndex] = item;
            GameObject itemSlotObject = inventorySlots[currentItemSlotIndex];
            Image imageComponent = itemSlotObject.GetComponent<Image>();

            // 이미지 컴포넌트가 없는 경우에는 추가하고 활성화합니다.
            if (imageComponent == null)
            {
                // Image 컴포넌트 추가
                imageComponent = itemSlotObject.AddComponent<Image>();
                // 이미지 스프라이트 설정
                imageComponent.sprite = item._icon;
            }

            currentItemSlotIndex++;
        }
        else
        {
            print("인벤토리에 자리가 없습니다.");
        }

    }

    // 아이템 아이톤 클릭시 이벤트
    public void OnItemClick()
    {
        // 마우스로 클릭한 UI 요소 가져오기
        GameObject clickedObject = EventSystem.current.currentSelectedGameObject;

        if (clickedObject != null)
        {
            // UI 요소의 자식 객체들 중에서 Image 컴포넌트를 찾기
            Image imageComponent = clickedObject.transform.GetChild(0).GetComponent<Image>();
            imageComponent.color = Color.white;
            if (imageComponent != null)
            {
                WeaponEquipment(clickedObject, imageComponent);
            }
        }
        else
        {
            Debug.Log("마우스로 클릭한 UI 요소가 없습니다.");
        }
    }
    
    // 인벤토리 아이템 캐릭터 상태창으로 이동 및 장착
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
        string pattern = @"\d+"; // 정규식 패턴: 연속된 숫자
        Match match = Regex.Match(input, pattern); // 정규식 패턴과 일치하는 부분 찾기

        if (match.Success) // 일치하는 부분이 있는 경우
        {
            return int.Parse(match.Value); // 일치하는 부분을 정수로 변환하여 반환
        }
        else
        {
            Debug.LogWarning("No number found in the input string.");
            return -1; // 일치하는 부분이 없는 경우 -1을 반환하거나 적절한 에러 처리를 수행할 수 있음
        }
    }

}
