using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum UpgradeType
{
    Armor,
    Speed,
    Attack,
    Ammo
}

public class Upgrade : MonoBehaviour
{
    // Singleton instance
    public static Upgrade Instance { get; private set; }

    Dictionary<UpgradeType, int> upgradeStats = new Dictionary<UpgradeType, int>()
    {
        {UpgradeType.Armor, 0},
        {UpgradeType.Speed, 0},
        {UpgradeType.Attack, 0},
        {UpgradeType.Ammo, 0},
    };

    // Add unity event for OnLevelUp
    public UnityEvent OnLevelUpEvent;

    public int GetStat(UpgradeType key)
    {
        if (upgradeStats.ContainsKey(key))
        {
            return upgradeStats[key];
        }
        return 0;
    }

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

    [SerializeField]
    int remainingPoints = 5;

    public bool RemainingPointsExist() => remainingPoints > 0;

    public void SpendPoints(UpgradeType key)
    {
        // increment the stat with the given key
        if (upgradeStats.ContainsKey(key))
        {
            upgradeStats[key]++;
            remainingPoints--;
        }
    }

    public void OnLevelUp()
    {
        remainingPoints += 5;
        OnLevelUpEvent?.Invoke();
    }
}
