using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class EndSceneManager : MonoBehaviour
{
    public Text stats;
    public Text className;
    public Image image;
    public List<Preset> presets;
    PlayerData playerData;

    void Start()
    {
        playerData = Data.Instance.LoadData();
        if (playerData != null)
        {
            className.text = playerData.className;
            stats.text = $"STR: {playerData.str}\n" +
                         $"DEX: {playerData.dex}\n" +
                         $"INT: {playerData.intl}\n" +
                         $"LUK: {playerData.luk}";

            // Load the preset image based on the class name
            Preset selectedPreset = presets.Find(p => p.playerData.className == playerData.className);
            if (selectedPreset != null)
            {
                image.sprite = selectedPreset.image;
            }
            else
            {
                Debug.LogWarning("Preset not found for class: " + playerData.className);
            }
        }
    }

    public void OnBackToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartScene");
    }
}
