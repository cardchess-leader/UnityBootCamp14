using System.Collections;
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
    public int MaxArmor = 10;
    public int MaxSpeed = 10;
    public int MaxAttack = 10;
    public int MaxAmmo = 10;

    public int GetStat(UpgradeType type)
    {
        switch (type)
        {
            case UpgradeType.Armor: return Armor;
            case UpgradeType.Speed: return Speed;
            case UpgradeType.Attack: return Attack;
            case UpgradeType.Ammo: return Ammo;
            default: return 0;
        }
    }

    public int GetMaxStat(UpgradeType type) {
        switch (type)
        {
            case UpgradeType.Armor: return MaxArmor;
            case UpgradeType.Speed: return MaxSpeed;
            case UpgradeType.Attack: return MaxAttack;
            case UpgradeType.Ammo: return MaxAmmo;
            default: return 0;
        }
    }
}

public class Upgrade : MonoBehaviour
{
    // Singleton instance
    public static Upgrade Instance { get; private set; }
    UpgradeStat upgradeStat = new UpgradeStat();
    public UnityEvent<UpgradeStat> OnStatUpdateEvent;
    //public UnityEvent<int> OnRemaingStatPointUpdateEvent;
    public int remainingStatPoints = 0;

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

    private void Start()
    {
        StartCoroutine(InitCoroutine());
    }

    IEnumerator InitCoroutine()
    {
        yield return null;
        //OnRemaingStatPointUpdateEvent?.Invoke(remainingStatPoints);
        OnStatUpdateEvent?.Invoke(upgradeStat);
    }

    public void IncrementUpgradeStat(UpgradeType type)
    {
        switch (type)
        {
            case UpgradeType.Armor: upgradeStat.Armor++; break;
            case UpgradeType.Speed: upgradeStat.Speed++; break;
            case UpgradeType.Attack: upgradeStat.Attack++; break;
            case UpgradeType.Ammo: upgradeStat.Ammo++; break;
        }
    }

    public void SpendPoints(UpgradeType key)
    {
        Debug.Log("SpendPoints: " + key);
        IncrementUpgradeStat(key);
        remainingStatPoints--;
        OnStatUpdateEvent?.Invoke(upgradeStat);
        //OnRemaingStatPointUpdateEvent?.Invoke(remainingStatPoints);
    }

    public void OnLevelUp()
    {
        remainingStatPoints += 5;
        OnStatUpdateEvent?.Invoke(upgradeStat);
    }
}
