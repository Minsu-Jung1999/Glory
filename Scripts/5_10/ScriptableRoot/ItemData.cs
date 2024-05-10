using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item Data", menuName = "Scriptable Object/Item Data", order = int.MaxValue)]
public class ItemData : ScriptableObject
{
    [SerializeField]
    private int itemId;
    public int _itemId { get { return itemId; } }

    [SerializeField]
    private bool isWeapon;
    public bool _isWeapon { get { return isWeapon; } }

    [SerializeField]
    public ItemType itemType;

    [SerializeField]
    public EquipMentSlotType equipMentType;


    [SerializeField]
    private GameObject itemOb;
    public GameObject _itemOb { get { return itemOb; } }

    [SerializeField]
    private string itemName;
    public string _itemName { get { return itemName; } }

    [SerializeField]
    private float dmg;
    public float _dmg { get { return dmg; } }

    [SerializeField]
    private Sprite icon;
    public Sprite _icon { get { return icon; } }

   
}
