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
     * 0 = �Ѽ� �� ��ġ (������)
     * 1 = Ȱ ��ġ (�޼�)
     * 2 = �ְ� ��ġ(������)
     * 3 = �ְ� ��ġ(�޼�)
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

    // ���⸦ �����ϴ� �Լ�
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
        // weaponLocation �迭�� �ڽ� ������Ʈ�� �ִٸ� ����
        foreach (GameObject location in weaponLocation)
        {
            if (location.transform.childCount > 0)
            {
                Destroy(location.transform.GetChild(0).gameObject);
            }
        }
    }
}
