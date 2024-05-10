using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;


[CreateAssetMenu(fileName = "Character Data", menuName = "Scriptable Object/Character Data", order = int.MaxValue)]
public class CharacterData : ScriptableObject
{
    [SerializeField]
    private string characterType;
    public string _characterType { get { return characterType; } }

    [SerializeField]
    private string characterName;
    public string _characterName { get {  return characterName; } }

    [SerializeField]
    private int hp;
    public int _hp { get { return hp; } }

    [SerializeField]
    private int dmg;
    public int _dmg { get {  return dmg; } }

    [SerializeField]
    private float walkSpeed;
    public float _walkSpeed { get {  return walkSpeed; } }

    [SerializeField]
    private float runSpeed;
    public float _runSpeed { get { return runSpeed; } }

    [SerializeField]
    private int rotateSpeed;
    public int _rotateSpeed { get { return rotateSpeed; } }

    [SerializeField]
    private float lookRotationSpeed;
    public float _lookRotationSpeed { get { return lookRotationSpeed; } }

    [SerializeField]
    private int coin;
    public int _coin { get { return coin; } }

}
