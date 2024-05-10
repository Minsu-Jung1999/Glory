using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public struct AcceptedQuest
{
    public string questName;
    public int questID;
    public string questInfo;
    public ItemData rewordItem;
    public int reworldCoin;
}

public class GameManager : MonoBehaviour
{

    public static GameManager instance; // 싱글톤을 할당할 전역 변수
    public bool isGameover = false; // 게임 오버 상태
    public GameObject gameoverUI; // 게임 오버시 활성화 할 UI 게임 오브젝트
    public List<AcceptedQuest> quests;
    public bool isCommunicating=false;
    public bool canTalk =false;

    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject interactionUI;

    [SerializeField]
    GameObject questWindowUI;

    [SerializeField]
    List<GameObject> questNPC;

    [SerializeField]
    GameObject inventoryManager;

    [SerializeField]
    TextMeshProUGUI coin_text;

    [SerializeField]
    Text playerName;

    private StatusManager playerstatus;


    // 게임 시작과 동시에 싱글톤을 구성
    void Awake()
    {
        // 싱글톤 변수 instance가 비어있는가?
        if (instance == null)
        {
            // instance가 비어있다면(null) 그곳에 자기 자신을 할당
            instance = this;
        }
        else
        {
            // instance에 이미 다른 GameManager 오브젝트가 할당되어 있는 경우
            // 씬에 두개 이상의 GameManager 오브젝트가 존재한다는 의미.
            // 싱글톤 오브젝트는 하나만 존재해야 하므로 자신의 게임 오브젝트를 파괴
            Debug.LogWarning("씬에 두 개 이상의 게임 매니저가 존재합니다!");
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject); //씬 이동해도 사라지지않게
    }

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            quests = new List<AcceptedQuest>();
            playerstatus = player.GetComponent<StatusManager>();
            coin_text.text = playerstatus.GetCurrentCoin().ToString();
            playerName.text = playerstatus.GetName().ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InteractionState(bool state)
    {
        interactionUI.SetActive(state);
    }

    public void questState(bool state) 
    {
        if(canTalk)
        {
            isCommunicating = true;
            questWindowUI.SetActive(state);
            Cursor.visible = state;
            Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    public void AddQuest(AcceptedQuest newQuest)
    {
        // quests 리스트의 모든 원소를 확인합니다.
        bool isUnique = true;
        foreach (AcceptedQuest quest in quests)
        {
            // 만약 quests.questID와 newQuests.questID가 같다면 겹치는 것이므로 추가하지 않습니다.
            if (quest.questID == newQuest.questID)
            {
                isUnique = false;
                break;
            }
        }

        // 겹치지 않는 경우에만 quests에 추가합니다.
        if (isUnique)
        {
            quests.Add(newQuest);
        }
    }

    public void IsCommunicating(bool state)
    {
        isCommunicating = state;
    }

    public void QuestSuccess(int questID)
    {
        bool findQuestID = false;
        for (int i = 0; i < questNPC.Count; i++)
        {
            GameObject _questNPC = questNPC[i];
            foreach (var quest in _questNPC.GetComponent<NPCManager>().GetDialogues().questDialogue)
            {
                if(quest.questId == questID)
                {
                    _questNPC.GetComponent<NPCManager>().QuestSucess();
                    if(quest.rewordItem != null)
                        inventoryManager.GetComponent<InventoryManager>().AddItem(quest.rewordItem);
                    findQuestID = true;
                    AddCoin(quest.rewordCoin);
                }
            }
        }
        if(findQuestID)
        {
            RemoveQuest(questID);
        }

    }

    private void RemoveQuest(int questID)
    {
        for (int i = 0; i < quests.Count; i++)
        {
            if (quests[i].questID == questID)
            {
                quests.RemoveAt(i);
                break; // 해당 퀘스트를 찾았으므로 루프 종료
            }
        }
    }

    public void AddCoin(int amount)
    {
        playerstatus.SetCoin(playerstatus.GetCurrentCoin() + amount);
        coin_text.text = playerstatus.GetCurrentCoin().ToString();
    }
}
