using UnityEngine;
using UnityEngine.Events;

public enum UpgradeType
{
    Armor,
    Speed,
    Attack,
    Ammo
}

public class UpgradeStat
{
    public int Armor;
    public int Speed;
    public int Attack;
    public int Ammo;


    public int GetStat(UpgradeType type)
    {
        switch(type)
        {
            case UpgradeType.Armor: return Armor;
            case UpgradeType.Speed: return Speed;
            case UpgradeType.Attack: return Attack;
            case UpgradeType.Ammo: return Ammo;
            default: return 0;
        }
    }

    public void IncrementUpgradeStat(UpgradeType type)
    {
        switch (type)
        {
            case UpgradeType.Armor: Armor++; break;
            case UpgradeType.Speed: Speed++; break;
            case UpgradeType.Attack: Attack++; break;
            case UpgradeType.Ammo: Ammo++; break;
        }
    }
}

public class Upgrade : MonoBehaviour
{
    // Singleton instance
    public static Upgrade Instance { get; private set; }

    UpgradeStat upgradeStat = new UpgradeStat();
    [SerializeField] PlayerStat playerStat;
    int remainingPoints = 0;

    public UnityEvent<UpgradeStat, PlayerStat> OnLevelUpEvent;
    public UnityEvent<UpgradeStat> OnLevelUpUIEvent;
    public UnityEvent<int>OnStatPointSpentEvent;

    public int GetStat(UpgradeType key)
    {
        return upgradeStat.GetStat(key);
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

    public bool RemainingPointsExist() => remainingPoints > 0;

    public void SpendPoints(UpgradeType key)
    {
        upgradeStat.IncrementUpgradeStat(key);
        remainingPoints--;
        OnStatPointSpentEvent?.Invoke(remainingPoints);
    }

    public void OnLevelUp()
    {
        remainingPoints += 5;
        OnLevelUpEvent?.Invoke(upgradeStat, playerStat);
        OnLevelUpUIEvent?.Invoke(upgradeStat);
    }
}
