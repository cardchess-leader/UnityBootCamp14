using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DropDownController : MonoBehaviour
{
    // Make this class a singleton
    public static DropDownController Instance { get; private set; }

    public List<Preset> presets;

    public Preset GetSelectedPreset
    {
        get { return presets[dropdown.value]; }
    }

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
                text = preset.playerData.className,
            };
            // Add the option to the dropdown
            dropdown.options.Add(option);
        }
        OnDropdownValueChanged();
    }

    // Method for handling dropdown value changes
    public void OnDropdownValueChanged()
    {
        UI.Instance.SetStatsText(GetSelectedPreset.playerData);
        UI.Instance.SetClassImage(GetSelectedPreset.image);
    }
}
