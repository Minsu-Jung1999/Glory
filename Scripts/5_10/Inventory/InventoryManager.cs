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
    
    // 장착한 아이템 아이콘
    [SerializeField]
    GameObject[] equipmentSlot;

    // 장착한 아이템에 정보 저장
    [SerializeField]
    ItemData[] equipmentItems;
 
    // 인벤토리 슬롯 저장
    [SerializeField]
    GameObject[] inventorySlots;

    // 인벤토리에 있는 아이템에 정보 저장
    [SerializeField]
    List<ItemData> itemData = new List<ItemData>();

    private void Start()
    {
        print("Inventory start");
        // 인벤토리에 있는 아이템에 저장 개수는 인벤토리 수와 같도록 설정
        equipmentItems = new ItemData[equipmentSlot.Length];
    }

    // 인벤토리에 아이템 추가
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

    // 아이템을 클릭시 인벤토리에 아이템을 장착 슬롯으로 이동
    // 장착 아이템 배열에도 추가
    public void EquipItem()
    {
        // 마우스로 클릭한 옵젝가져오기
        GameObject clickedObject = EventSystem.current.currentSelectedGameObject;

        // 클릭한 오브젝트에 배열 위치 저장
        int index = 0;

        // 인벤토리에서 아이템 슬롯을 찾아 장착 슬롯으로 이동
        foreach(GameObject slot in inventorySlots)
        {
            // 클릭한 오브젝트를 인벤토리에서 찾기
            if(clickedObject == slot)
            {
                ItemData item = itemData[index];

                // 이미지 컴포넌트 비활성화
                slot.GetComponent<Image>().enabled = false; 

                // 만약 장착 슬롯에 아이템이 있다면 장착 하고 있는 아이템을 인벤토리에 추가 한다
                if(equipmentItems[(int)item.itemType] != null)
                {
                    AddItem(equipmentItems[(int)item.itemType]);
                }

                // 클릭한 오브젝트를 장착 슬롯에 추가하기
                equipmentItems[(int)item.itemType] = item;
                itemManager.EquipWeapon(item);
                
                // 장착 슬롯에 이미지 추가하기
                equipmentSlot[(int)item.itemType].GetComponent<Image>().sprite = item._icon;

                // 인벤토리에 있는 아이템은 장착 슬롯으로 이동했기 때문에 인벤토리 아이템 배열에서 해당 아이템을 삭제 해 준다.
                itemData.RemoveAt(index);
                break;
            }
            index++;
        }

        // 아이템을 장착할 때 인벤토리 아이템이 정렬 되게 해 준다
        SortItem(index);
    }

    // 인벤토리를 모두 비활성화 한 후 인벤토리 아이템 리스트를 기준으로 아이템을 다시 정렬한다
    private void SortItem(int startIndex)
    {
        // 인벤토리 아이템 모두 비활성화
        foreach (GameObject slot in inventorySlots)
        {
            slot.GetComponent<Image>().enabled = false;
        }

        // 아이템데이터 배열을 사용해서 인벤토리 슬롯을 정렬한다.
        for(int i = 0; i < itemData.Count; i++)
        {
            Image itemIcon = inventorySlots[i].GetComponent<Image>();
            itemIcon.sprite = itemData[i]._icon;
            itemIcon.enabled = true;
        }
    }






}
