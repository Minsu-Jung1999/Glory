using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public struct QuestLogUISetting
{
    [SerializeField]
    public Text questName;
    [SerializeField]
    public Text questInfo;
    [SerializeField]
    public Image questItemIcon;
    [SerializeField]
    public Text questItemName;
    [SerializeField]
    public Text questGoldReword;
}

public class QuestLogManager : MonoBehaviour
{
    [SerializeField]
    GameObject questLog;

    [SerializeField]
    GameObject contents;

    [SerializeField]
    QuestLogUISetting questLogUI;

    [SerializeField]
    Button questLogButton;

    // ����Ʈ �αװ� ����� �� ���� ȣ��
    public void AddQuestToLog()
    {
        // contents ������Ʈ�� �ڽĵ��� ��� �����ɴϴ�.
        GameObject[] children = contents.transform.Cast<Transform>().Select(t => t.gameObject).ToArray();

        // ù ��° �ڽ� GameObject�� ������ ��� �ڽ� ��ü�� �����մϴ�.
        foreach (GameObject child in children.Skip(1))
        {
            Destroy(child);
        }

        // GameManager�� �ν��Ͻ��� ���� quests ����Ʈ�� ��� ����ŭ questLog ������Ʈ�� �����մϴ�.
        for (int i = 0; i < GameManager.instance.quests.Count; i++)
        {
            GameObject questlog = Instantiate(questLog, contents.transform);
            questlog.GetComponentInChildren<Text>().text = GameManager.instance.quests[i].questName;
            InitButton(questlog);
        }
    }

    private Button InitButton(GameObject questlog)
    {
        Button button = questlog.GetComponent<Button>();
        button.onClick.AddListener(delegate { ShowQuestInfo(); });
        return button;
    }

    public void ShowQuestInfo()
    {
        print("Clicked");
        GameObject clickedObject = EventSystem.current.currentSelectedGameObject;
        GameObject content = clickedObject.transform.parent.gameObject;
        for (int i = 0; i < content.transform.childCount; i++)
        {
            GameObject log = content.transform.GetChild(i).gameObject;
            // log�� ���� �۾� ����
            if(log == clickedObject)
            {
                AcceptedQuest clickedQuest =  GameManager.instance.quests[i-1];
                if(questLogUI.questName == null)
                {
                    print("QuestLogUI.questName component is missing");
                }
                else
                {
                    questLogUI.questName.text = clickedQuest.questName;
                    questLogUI.questInfo.text = clickedQuest.questInfo;
                    if(clickedQuest.rewordItem != null)
                    {
                        questLogUI.questItemName.text = clickedQuest.rewordItem._itemName;
                        questLogUI.questItemIcon.enabled = true;
                        questLogUI.questItemIcon.sprite = clickedQuest.rewordItem._icon;
                        questLogUI.questGoldReword.text = " ���� ��� : " + clickedQuest.reworldCoin.ToString();
                        questLogUI.questGoldReword.color = Color.yellow;
                    }
                    else
                    {
                        questLogUI.questItemName.text = "���� ����";
                        questLogUI.questItemIcon.enabled = false;
                    }
                }

            }
        }
    }

    private void OnEnable()
    {
        questLogUI.questName.text = "";
        questLogUI.questItemName.text = "";
        questLogUI.questInfo.text = "";
        questLogUI.questItemIcon.enabled = false;
        questLogUI.questGoldReword.text = " ";

    }


}
