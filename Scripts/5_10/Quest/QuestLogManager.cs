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

    // 퀘스트 로그가 띄워질 때 마다 호출
    public void AddQuestToLog()
    {
        // contents 오브젝트의 자식들을 모두 가져옵니다.
        GameObject[] children = contents.transform.Cast<Transform>().Select(t => t.gameObject).ToArray();

        // 첫 번째 자식 GameObject를 제외한 모든 자식 객체를 제거합니다.
        foreach (GameObject child in children.Skip(1))
        {
            Destroy(child);
        }

        // GameManager의 인스턴스를 통해 quests 리스트의 요소 수만큼 questLog 오브젝트를 생성합니다.
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
            // log에 대한 작업 수행
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
                        questLogUI.questGoldReword.text = " 보상 골드 : " + clickedQuest.reworldCoin.ToString();
                        questLogUI.questGoldReword.color = Color.yellow;
                    }
                    else
                    {
                        questLogUI.questItemName.text = "보상 없음";
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
