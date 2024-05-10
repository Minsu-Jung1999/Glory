using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : MonoBehaviour
{
    [SerializeField]
    public int questID;

    public void CommunicateQuest()
    {
        for (int i = 0; i < GameManager.instance.quests.Count; i++)
        {
            var quest = GameManager.instance.quests[i];
            if (quest.questID == questID)
            {
                GameManager.instance.QuestSuccess(questID);
                return; // �ش� ����Ʈ�� ã�����Ƿ� �޼��� ����
            }
        }
        print("����Ʈ ����� �ƴմϴ�.");
    }

}
