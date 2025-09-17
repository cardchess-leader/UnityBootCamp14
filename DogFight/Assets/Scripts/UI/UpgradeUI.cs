using System.Collections;
using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField]
    UpgradeRow[] upgradeRows = new UpgradeRow[4]; // 0: Armor, 1: Speed, 2: Attack, 3: Ammo

    private void Start()
    {
        Upgrade.Instance.OnLevelUpUIEvent.AddListener(UpdateProgressBars);
    }

    public void OnUpgrade(UpgradeType upgradeType)
    {
        Upgrade.Instance.SpendPoints(upgradeType);
        //UpdateProgressBars();
    }

    void UpdateProgressBars(UpgradeStat upgradeStat)
    {
        bool remainingPointsExist = Upgrade.Instance.RemainingPointsExist();
        foreach (UpgradeRow row in upgradeRows)
        {
            int statValue = upgradeStat.GetStat(row.upgradeType);
            row.SetProgressValue(statValue, remainingPointsExist);
        }
    }
}
