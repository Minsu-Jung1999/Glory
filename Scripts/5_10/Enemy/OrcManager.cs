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
        // Random 객체 생성
        System.Random random = new System.Random();

        // 1부터 5까지의 랜덤한 정수 생성
        int patternNumber = random.Next(0, 5);

        // 정수 값을 열거형의 값으로 변환
        Pattern pattern = (Pattern)patternNumber;

        switch (pattern)
        {
            case Pattern.patrol:
                print("Patroll 중");
                yield return new WaitForSeconds(2f);
                break;
            case Pattern.attack_default:
                print("attack_default 중");
                yield return new WaitForSeconds(2f);
                break;
            case Pattern.shield:
                print("shield 중");
                yield return new WaitForSeconds(2f);
                break;
            case Pattern.attack_dodge:
                print("attack_dodge 중");
                yield return new WaitForSeconds(2f);
                break;
            case Pattern.attack_heavy:
                print("attack_heavy 중");
                yield return new WaitForSeconds(2f);
                break;
            default:
                break;
        }
    }
}
