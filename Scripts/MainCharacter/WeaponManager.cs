using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
#region
    // TEST
    [SerializeField]
    ItemData onehandSowrd;
    [SerializeField]
    GameObject weaponEquipmentSlot;
#endregion





    [SerializeField]
    GameObject itemSlotsContent;

    [SerializeField]
    List<GameObject> itemSlots;

    [SerializeField]
    private List<ItemData> inventoryItems;

    private int itemSlotIndex = 0;

    private void Start()
    {
        if (itemSlotsContent == null)
        {
            Debug.LogWarning("Slot content is not assigned.");
            return;
        }

        // Slot content의 자식 게임 오브젝트들을 가져옵니다.
        Transform[] childTransforms = itemSlotsContent.GetComponentsInChildren<Transform>(true);

        // 리스트 초기화
        itemSlots = new List<GameObject>();

        foreach (Transform childTransform in childTransforms)
        {
            // Slot content 자체는 제외하고, 자식의 자식은 리스트에 추가하지 않습니다.
            if (childTransform.parent.name == "Content" && childTransform.gameObject != itemSlotsContent)
            {
                itemSlots.Add(childTransform.gameObject);
            }
        }

        // 결과를 로그로 출력합니다.
        Debug.Log("Total itemSlots: " + itemSlots.Count);

        //TEST
        AddItemInInventory(onehandSowrd);
        AddItemInInventory(onehandSowrd);
    }

    public void AddItemInInventory(ItemData item)
    {
        GameObject Weapon = item._item;

        if (itemSlotIndex < itemSlots.Count)
        {
            inventoryItems.Add(item);
            // 자식 오브젝트에서 Image 컴포넌트를 가져옵니다.
            GameObject itemSlotObject = itemSlots[itemSlotIndex].transform.GetChild(0).gameObject;
            Image imageComponent = itemSlotObject.GetComponentInChildren<Image>();

            // 이미지 컴포넌트가 없는 경우에는 추가하고 활성화합니다.
            if (imageComponent == null)
            {
                // Image 컴포넌트 추가
                imageComponent = itemSlotObject.AddComponent<Image>();
                // 이미지 스프라이트 설정
                imageComponent.sprite = item._icon;
            }

            // 이미지 활성화
            imageComponent.gameObject.SetActive(true);
            itemSlotIndex++;
        }
        else
        {
            print("인벤토리에 자리가 없습니다.");
        }

    }
    public void OnItemClick()
    {
        // 마우스로 클릭한 UI 요소 가져오기
        GameObject clickedObject = EventSystem.current.currentSelectedGameObject;

        if (clickedObject != null)
        {
            // UI 요소의 자식 객체들 중에서 Image 컴포넌트를 찾기
            Image imageComponent = clickedObject.GetComponentInChildren<Image>();
            imageComponent.color = Color.white;
            if (imageComponent != null)
            {
                weaponEquipmentSlot.transform.GetChild(0).GetComponentInChildren<Image>().sprite = imageComponent.sprite;
                imageComponent.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("이 UI 요소에는 Image 컴포넌트가 없습니다.");
            }
        }
        else
        {
            Debug.Log("마우스로 클릭한 UI 요소가 없습니다.");
        }
    }

}
