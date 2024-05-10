using Cinemachine;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

public class StatusManager : MonoBehaviour
{
    [SerializeField]
    CharacterData characterData;

    [SerializeField]
    Slider hpBar;

    private string playerName;
    private float currentHP;
    private float currentRunSpeed;
    private float currentWalkSpeed;
    private float currentRotateSpeed;
    private float currentLookRotationSpeed;
    private float currentDmg;
    private int coin;
    private int level;

    private void Start()
    {
        currentHP = characterData._hp;
        currentRunSpeed = characterData._runSpeed;
        currentRotateSpeed = characterData._rotateSpeed;
        currentWalkSpeed = characterData._walkSpeed;
        currentDmg = characterData._dmg;
        playerName = characterData._characterName;
        coin = characterData._coin;
        hpBar.maxValue = characterData._hp;
        hpBar.value = characterData._hp;
    }

    // ���� ü�� ��ȯ
    public float GetCurrentHP()
    {
        return currentHP;
    }
    public void SetHP( float hp)
    {
        currentHP = hp;
    }

    // �ִ� ü�� ��ȯ
    public float GetMaxHP()
    {
        return characterData._hp;
    }

    // �ǰ� ����
    public void GetHit(float hitDamage)
    {
        currentHP -= hitDamage;
        hpBar.value = currentHP;

        if(currentHP <= 0)      // �׽�Ʈ
        {
            GoblinMovement goblin = GetComponent<GoblinMovement>();
            goblin.OnDeath();
        }
    }

    // �ȱ�ӵ� ��ȯ
    public float GetCurrentRunSpeed()
    {
        return currentRunSpeed;
    }
    public void SetRunSpeed( float runSpeed)
    {
        currentRunSpeed = runSpeed;
    }

    // �޸��� �ӵ� ��ȯ
    public float GetCurrentWalkSpeed()
    {
        return currentWalkSpeed;
    }
    public void SetWalkSpeed(float walkSpeed)
    {
        currentWalkSpeed = walkSpeed;
    }

    // ȸ�� �ӵ� ��ȯ
    public float GetCurrentRotateSpeed()
    {
        return currentRotateSpeed;
    }
    public void SetRotateSpeed(float rotateSpeed)
    {
        currentRotateSpeed = rotateSpeed;
    }

    // ���콺 ȸ�� �ӵ� ��ȯ
    public float GetCurrentLookRotationSpeed()
    {
        return currentLookRotationSpeed;
    }
    public void SetLookRotationSpeed(float lookRotationSpeed)
    {
        currentLookRotationSpeed = lookRotationSpeed;
    }

    // ���ݷ� ��ȯ
    public float GetCurrentDMG()
    {
        return currentDmg;
    }
    public void SetDMG(float dmg)
    {
        currentDmg = dmg;
    }

    // ���� ��ȯ
    public int GetCurrentCoin()
    {
        return coin;
    }
    public void SetCoin(int coin)
    {
        this.coin = coin;
    }

    // �̸� ��ȯ
    public string GetName()
    {
        return playerName;
    }
    public void SetName(string name)
    {
        playerName = name; 
    }

    // ���� ��ȯ
    public int GetCurrentLevel()
    {
        return level;
    }
    public  void SetLevel(int _level)
    {
        level = _level;
    }
}
