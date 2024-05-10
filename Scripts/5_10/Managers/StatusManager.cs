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

    // 현재 체력 반환
    public float GetCurrentHP()
    {
        return currentHP;
    }
    public void SetHP( float hp)
    {
        currentHP = hp;
    }

    // 최대 체력 반환
    public float GetMaxHP()
    {
        return characterData._hp;
    }

    // 피격 상태
    public void GetHit(float hitDamage)
    {
        currentHP -= hitDamage;
        hpBar.value = currentHP;

        if(currentHP <= 0)      // 테스트
        {
            GoblinMovement goblin = GetComponent<GoblinMovement>();
            goblin.OnDeath();
        }
    }

    // 걷기속도 반환
    public float GetCurrentRunSpeed()
    {
        return currentRunSpeed;
    }
    public void SetRunSpeed( float runSpeed)
    {
        currentRunSpeed = runSpeed;
    }

    // 달리기 속도 반환
    public float GetCurrentWalkSpeed()
    {
        return currentWalkSpeed;
    }
    public void SetWalkSpeed(float walkSpeed)
    {
        currentWalkSpeed = walkSpeed;
    }

    // 회전 속도 반환
    public float GetCurrentRotateSpeed()
    {
        return currentRotateSpeed;
    }
    public void SetRotateSpeed(float rotateSpeed)
    {
        currentRotateSpeed = rotateSpeed;
    }

    // 마우스 회전 속도 반환
    public float GetCurrentLookRotationSpeed()
    {
        return currentLookRotationSpeed;
    }
    public void SetLookRotationSpeed(float lookRotationSpeed)
    {
        currentLookRotationSpeed = lookRotationSpeed;
    }

    // 공격력 반환
    public float GetCurrentDMG()
    {
        return currentDmg;
    }
    public void SetDMG(float dmg)
    {
        currentDmg = dmg;
    }

    // 코인 반환
    public int GetCurrentCoin()
    {
        return coin;
    }
    public void SetCoin(int coin)
    {
        this.coin = coin;
    }

    // 이름 반환
    public string GetName()
    {
        return playerName;
    }
    public void SetName(string name)
    {
        playerName = name; 
    }

    // 레벨 반환
    public int GetCurrentLevel()
    {
        return level;
    }
    public  void SetLevel(int _level)
    {
        level = _level;
    }
}
