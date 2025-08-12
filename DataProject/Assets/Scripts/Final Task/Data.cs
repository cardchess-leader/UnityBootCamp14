using System;
using System.IO;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public string className;
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
        playerData = LoadData();
        if (playerData == null)
        {
            UI.Instance.DisableContinueBtn();
        }
    }

    public PlayerData LoadData()
    {
        if (File.Exists(SaveFilePath))
        {
            string json = File.ReadAllText(SaveFilePath);
            return JsonUtility.FromJson<PlayerData>(json);
        }
        else
        {
            return null;
        }
    }

    public void SaveData(PlayerData playerData)
    {
        string json = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(SaveFilePath, json);
        // Move to next Scene
        UI.Instance.ContinueGame();
    }

    public void ClearData()
    {
        if (File.Exists(SaveFilePath))
        {
            File.Delete(SaveFilePath);
        }
        playerData = null;
        UI.Instance.DisableContinueBtn();
    }
}
