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

        // ���� �������� ���� ��
        if(_questdialogue.rewordItem != null)
        {
            questUI.questRewordItemNameUI.GetComponent<Text>().text = _questdialogue.rewordItem._itemName;
            questUI.questRewordItemInfoUI.GetComponent<Text>().text = "DMG : " + _questdialogue.rewordItem._dmg.ToString();
            questUI.questGoldRewordUI.GetComponent<Text>().text = "���� ��� : " + _questdialogue.rewordCoin.ToString();
            questUI.questGoldRewordUI.GetComponent<Text>().color = Color.yellow;
            questUI.questItemIcon.GetComponent<Image>().enabled = true;
            questUI.questItemIcon.GetComponent<Image>().sprite = _questdialogue.rewordItem._icon;
        }
        // ���� �������� ���� ��
        else
        {
            questUI.questRewordItemNameUI.GetComponent<Text>().text = "���� ������ ����";
            questUI.questRewordItemInfoUI.GetComponent<Text>().text = "";
            questUI.questItemIcon.GetComponent<Image>().enabled = false;
        }
    }

    public void QuestAccept()
    {
        // ���ο� AcceptedQuest ����
        AcceptedQuest newQuest = new AcceptedQuest();
        // AcceptedQuest�� �ʵ���� ä��ϴ�.
        newQuest.questInfo = questDialogue.QuestsInfo;
        newQuest.questID = questDialogue.questId;
        newQuest.rewordItem = questDialogue.rewordItem;
        newQuest.questName = questDialogue.questName;
        newQuest.reworldCoin = questDialogue.rewordCoin;

        // GameManager�� �ν��Ͻ��� �����Ͽ� quests ����Ʈ�� �߰�
        GameManager.instance.AddQuest(newQuest);
        questLog.AddQuestToLog();
    }
}
