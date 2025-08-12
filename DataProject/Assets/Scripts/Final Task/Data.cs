using System;
using System.IO;
using UnityEngine;
using static JsonMaker;

[Serializable]
public class PlayerData
{
    public int str;
    public int dex;
    public int intl;
    public int luk;
}

public class Data : MonoBehaviour
{
    // Make this class a singleton
    public static Data Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    const string saveFileName = "saveData.json";
    string SaveFilePath => Path.Combine(Application.persistentDataPath, saveFileName);

    PlayerData playerData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadData();
    }

    void LoadData()
    {
        if (File.Exists(SaveFilePath))
        {
            string json = File.ReadAllText(SaveFilePath);
            playerData = JsonUtility.FromJson<PlayerData>(json);
        }
        else
        {
            UI.Instance.DisableContinueBtn();
        }
    }

    public void SaveData(PlayerData playerData)
    {
        string json = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(SaveFilePath, json);
        // Move to next Scene
        UI.Instance.ContinueGame();
    }
}
