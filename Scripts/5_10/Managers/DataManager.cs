using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using UnityEngine;


public class DataManager : MonoBehaviour
{
    // 싱글톤
    public static DataManager instance;

    // 파일 경로
    string path;
    string fileName = "save";

    // 저장 파일
    [SerializeField]
    GameObject player;
    SavedData saveData = new SavedData();
    private StatusManager playerStatus;

    

    private void Awake()
    {
        #region 싱글톤
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this) 
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        #endregion
        path = Application.persistentDataPath + "/";
    }

    private void Start()
    {
        if(player)
            playerStatus = player.GetComponent<StatusManager>();
    }

    public void SaveData()
    {
        if (player != null)
        {
            SavePlayerInfo();
            string data = JsonUtility.ToJson(saveData);

            print(path);

            File.WriteAllText(path + fileName, data);
        }
        else
        {
            print("Player GameObject가 없어 대이터를 저장할 수 없습니다.");
        }
    }

    public void LoadData()
    {
        print(path);
        string data = File.ReadAllText(path + fileName);
        saveData = JsonUtility.FromJson<SavedData>(data);
        LoadPlayerInfo();
    }

    private void SavePlayerInfo()
    {
        saveData.name = playerStatus.GetName();
        saveData.level = playerStatus.GetCurrentLevel();
        saveData.hp = playerStatus.GetCurrentHP();
        saveData.dmg = playerStatus.GetCurrentDMG();
        saveData.walkSpeed = playerStatus.GetCurrentWalkSpeed();
        saveData.runSpeed = playerStatus.GetCurrentRunSpeed();
        saveData.rotateSpeed = playerStatus.GetCurrentRotateSpeed();
        saveData.lookRotationSpeed = playerStatus.GetCurrentRotateSpeed();
        saveData.coin = playerStatus.GetCurrentCoin();
        saveData.playerPosition = player.transform.position;
        saveData.playerRotation = player.transform.eulerAngles;
    }

    private void LoadPlayerInfo()
    {
        playerStatus.SetName(saveData.name);
        playerStatus.SetLevel(saveData.level);
        playerStatus.SetHP(saveData.hp);
        playerStatus.SetDMG(saveData.dmg);
        playerStatus.SetWalkSpeed(saveData.walkSpeed);
        playerStatus.SetRunSpeed(saveData.runSpeed);
        playerStatus.SetRotateSpeed(saveData.rotateSpeed);
        playerStatus.SetLookRotationSpeed(saveData.lookRotationSpeed);
        playerStatus.SetCoin(saveData.coin);

        player.transform.position = saveData.playerPosition;
        player.transform.eulerAngles = saveData.playerRotation;
    }

}
