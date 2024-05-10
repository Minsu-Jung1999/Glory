using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    /*
     * 0 = 한손 검 위치 (오른손)
     * 1 = 활 위치 (왼손)
     * 2 = 쌍검 위치(오른손)
     * 3 = 쌍검 위치(왼손)
     */
    public GameObject[] weaponLocation;

    public AttackCollision rightAttackCollision;
    public AttackCollision leftAttackCollision;

    private MainCharacterMovement mc_MovementSC;
    private MainCharacterAnimatorControll mc_animControllSC;

    private void Start()
    {
        mc_MovementSC = player.GetComponent<MainCharacterMovement>();  
        mc_animControllSC = player.GetComponent<MainCharacterAnimatorControll>();
    }

    // 무기를 장착하는 함수
    public void EquipWeapon(ItemData item)
    {
        ExsistingWeaponInitializing(item);

        GameObject rightHandItem = Instantiate(item._itemOb, weaponLocation[(int)item.equipMentType].transform);
        rightAttackCollision = rightHandItem.transform.GetComponent<AttackCollision>();

        if (item.equipMentType == EquipMentSlotType.TwoHand)
        {

            GameObject leftHandItem = Instantiate(item._itemOb, weaponLocation[(int)item.equipMentType + 1].transform);
            leftAttackCollision = leftHandItem.transform.GetComponent<AttackCollision>();
        }
        else
        {
            leftAttackCollision = null;
        }

        mc_MovementSC.isArmed = true;
        mc_animControllSC.ArmedWeapon(item._itemId);
    }

    private void ExsistingWeaponInitializing(ItemData item)
    {
        // weaponLocation 배열에 자식 오브젝트가 있다면 제거
        foreach (GameObject location in weaponLocation)
        {
            if (location.transform.childCount > 0)
            {
                Destroy(location.transform.GetChild(0).gameObject);
            }
        }
    }
}
