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
                return; // 해당 퀘스트를 찾았으므로 메서드 종료
            }
        }
        print("퀘스트 대상이 아닙니다.");
    }

}
