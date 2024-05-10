using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Pattern
{
    patrol=1,
    attack_default,
    shield,
    attack_dodge,
    attack_heavy,
}

public class OrcManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator BossPatern()
    {
        // Random ��ü ����
        System.Random random = new System.Random();

        // 1���� 5������ ������ ���� ����
        int patternNumber = random.Next(0, 5);

        // ���� ���� �������� ������ ��ȯ
        Pattern pattern = (Pattern)patternNumber;

        switch (pattern)
        {
            case Pattern.patrol:
                print("Patroll ��");
                yield return new WaitForSeconds(2f);
                break;
            case Pattern.attack_default:
                print("attack_default ��");
                yield return new WaitForSeconds(2f);
                break;
            case Pattern.shield:
                print("shield ��");
                yield return new WaitForSeconds(2f);
                break;
            case Pattern.attack_dodge:
                print("attack_dodge ��");
                yield return new WaitForSeconds(2f);
                break;
            case Pattern.attack_heavy:
                print("attack_heavy ��");
                yield return new WaitForSeconds(2f);
                break;
            default:
                break;
        }
    }
}
