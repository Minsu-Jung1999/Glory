using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Dialogue;

[System.Serializable]
public struct QuestUI
{
    [SerializeField]
    public GameObject questInfoUI;

    [SerializeField]
    public GameObject questRewordItemNameUI;
    [SerializeField]
    public GameObject questGoldRewordUI;

    [SerializeField]
    public GameObject questRewordItemInfoUI;
    
    [SerializeField]
    public GameObject questItemIcon;

    [SerializeField]
    public GameObject questName;
}

public class QuestUIManager : MonoBehaviour
{

    [SerializeField]
    QuestUI questUI;

    [SerializeField]
    QuestLogManager questLog;

    private Dialogue.QuestsDialogue questDialogue;
    
    public void QuestUISetting(Dialogue.QuestsDialogue _questdialogue)
    {
        questDialogue = _questdialogue;
        questUI.questName.GetComponent<Text>().text = _questdialogue.questName;
        questUI.questInfoUI.GetComponent<Text>().text = _questdialogue.QuestsInfo;

        // 보상 아이템이 있을 때
        if(_questdialogue.rewordItem != null)
        {
            questUI.questRewordItemNameUI.GetComponent<Text>().text = _questdialogue.rewordItem._itemName;
            questUI.questRewordItemInfoUI.GetComponent<Text>().text = "DMG : " + _questdialogue.rewordItem._dmg.ToString();
            questUI.questGoldRewordUI.GetComponent<Text>().text = "보상 골드 : " + _questdialogue.rewordCoin.ToString();
            questUI.questGoldRewordUI.GetComponent<Text>().color = Color.yellow;
            questUI.questItemIcon.GetComponent<Image>().enabled = true;
            questUI.questItemIcon.GetComponent<Image>().sprite = _questdialogue.rewordItem._icon;
        }
        // 보상 아이템이 없을 때
        else
        {
            questUI.questRewordItemNameUI.GetComponent<Text>().text = "보상 아이템 없음";
            questUI.questRewordItemInfoUI.GetComponent<Text>().text = "";
            questUI.questItemIcon.GetComponent<Image>().enabled = false;
        }
    }

    public void QuestAccept()
    {
        // 새로운 AcceptedQuest 생성
        AcceptedQuest newQuest = new AcceptedQuest();
        // AcceptedQuest의 필드들을 채웁니다.
        newQuest.questInfo = questDialogue.QuestsInfo;
        newQuest.questID = questDialogue.questId;
        newQuest.rewordItem = questDialogue.rewordItem;
        newQuest.questName = questDialogue.questName;
        newQuest.reworldCoin = questDialogue.rewordCoin;

        // GameManager의 인스턴스에 접근하여 quests 리스트에 추가
        GameManager.instance.AddQuest(newQuest);
        questLog.AddQuestToLog();
    }
}
