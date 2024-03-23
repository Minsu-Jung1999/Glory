using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
        GameObject rightHandItem = Instantiate(item._itemOb, weaponLocation[(int)item.equipMentType].transform);
        if (item.equipMentType == EquipMentSlotType.TwoHand)
        {
            GameObject leftHandItem = Instantiate(item._itemOb, weaponLocation[(int)item.equipMentType+1].transform);
        }
        else
        {
            leftAttackCollision = null;
        }
        mc_MovementSC.isArmed = true;
        mc_animControllSC.ArmedWeapon(item._itemId);
        print("Weapon ID : " + item._itemId); 
    }

   
}
