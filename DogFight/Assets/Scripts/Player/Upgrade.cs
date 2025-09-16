using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    // Singleton instance
    public static Upgrade Instance { get; private set; }

    Dictionary<string, int> upgradeStats = new Dictionary<string, int>()
    {
        {"Armor", 0},
        {"Speed", 0},
        {"Attack", 0},
        {"Ammo", 0},
    };

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
    }

    int remainingPoints = 0;
    
    public bool SpendPoints(string key)
    {
        // increment the stat with the given key
        if (upgradeStats.ContainsKey(key))
        {
            upgradeStats[key]++;
            remainingPoints--;
            OnRemainingPointsChanged();
        }
        if (remainingPoints == 0) 
            return false;
        return true;
    }

    void OnRemainingPointsChanged()
    {
        // Notify UI or other systems about the change in remaining points
        Debug.Log($"Remaining upgrade points: {remainingPoints}");
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
