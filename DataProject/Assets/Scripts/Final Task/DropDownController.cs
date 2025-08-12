using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DropDownController : MonoBehaviour
{
    // Make this class a singleton
    public static DropDownController Instance { get; private set; }

    public List<Preset> presets;
    public Text strText;
    public Text dexText;
    public Text intlText;
    public Text lukText;

    Dropdown dropdown;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // initialize each dropdown to have 1~6 options
    void Start()
    {
        dropdown = GetComponent<Dropdown>();
        for (int i = 0; i < presets.Count; i++)
        {
            Preset preset = presets[i];
            Dropdown.OptionData option = new Dropdown.OptionData
            {
                text = preset.presetName
            };
            // Add the option to the dropdown
            dropdown.options.Add(option);
        }
        SetStatsText();
    }

    public PlayerData GetSelectedPresetData()
    {
        // Fetch the selected option's index
        int index = dropdown.value;
        return presets[index].playerData;
    }

    // Method for handling dropdown value changes
    public void OnDropdownValueChanged()
    {
        SetStatsText();
    }

    void SetStatsText()
    {
        // Get the selected preset data
        PlayerData selectedData = GetSelectedPresetData();
        // Update the UI texts with the selected preset's data
        strText.text = "STR: " + selectedData.str;
        dexText.text = "DEX: " + selectedData.dex;
        intlText.text = "INT: " + selectedData.intl;
        lukText.text = "LUK: " + selectedData.luk;
    }
}
