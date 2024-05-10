using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Scriptable Object/Dialogue", order = int.MaxValue)]
public class Dialogue : ScriptableObject
{
    [System.Serializable]
    public class QuestsDialogue
    {
        public int questId;
        public string questName;
        // 퀘스트 보상 아이템
        public ItemData rewordItem;
        public int rewordCoin;
        [TextArea(3, 10)]
        public string QuestsInfo;
        public bool isComplate;
    }

    public QuestsDialogue[] questDialogue;
}
