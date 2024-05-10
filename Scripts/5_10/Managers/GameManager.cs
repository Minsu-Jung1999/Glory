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

    public static GameManager instance; // �̱����� �Ҵ��� ���� ����
    public bool isGameover = false; // ���� ���� ����
    public GameObject gameoverUI; // ���� ������ Ȱ��ȭ �� UI ���� ������Ʈ
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


    // ���� ���۰� ���ÿ� �̱����� ����
    void Awake()
    {
        // �̱��� ���� instance�� ����ִ°�?
        if (instance == null)
        {
            // instance�� ����ִٸ�(null) �װ��� �ڱ� �ڽ��� �Ҵ�
            instance = this;
        }
        else
        {
            // instance�� �̹� �ٸ� GameManager ������Ʈ�� �Ҵ�Ǿ� �ִ� ���
            // ���� �ΰ� �̻��� GameManager ������Ʈ�� �����Ѵٴ� �ǹ�.
            // �̱��� ������Ʈ�� �ϳ��� �����ؾ� �ϹǷ� �ڽ��� ���� ������Ʈ�� �ı�
            Debug.LogWarning("���� �� �� �̻��� ���� �Ŵ����� �����մϴ�!");
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject); //�� �̵��ص� ��������ʰ�
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
        // quests ����Ʈ�� ��� ���Ҹ� Ȯ���մϴ�.
        bool isUnique = true;
        foreach (AcceptedQuest quest in quests)
        {
            // ���� quests.questID�� newQuests.questID�� ���ٸ� ��ġ�� ���̹Ƿ� �߰����� �ʽ��ϴ�.
            if (quest.questID == newQuest.questID)
            {
                isUnique = false;
                break;
            }
        }

        // ��ġ�� �ʴ� ��쿡�� quests�� �߰��մϴ�.
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
                break; // �ش� ����Ʈ�� ã�����Ƿ� ���� ����
            }
        }
    }

    public void AddCoin(int amount)
    {
        playerstatus.SetCoin(playerstatus.GetCurrentCoin() + amount);
        coin_text.text = playerstatus.GetCurrentCoin().ToString();
    }
}
