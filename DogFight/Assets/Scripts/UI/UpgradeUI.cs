using System.Collections;
using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField]
    UpgradeRow[] upgradeRows = new UpgradeRow[4]; // 0: Armor, 1: Speed, 2: Attack, 3: Ammo

    private void Start()
    {
        Upgrade.Instance.OnStatUpdateEvent.AddListener(UpdateProgressBars);
    }

    public void OnUpgrade(UpgradeType upgradeType)
    {
        Upgrade.Instance.SpendPoints(upgradeType);
    }

    void UpdateProgressBars(UpgradeStat upgradeStat)
    {
        int remaingStatPoint = Upgrade.Instance.remainingStatPoints;
        for (int i = 0; i < 4; i++)
        {
            var row = upgradeRows[i];
            int statValue = upgradeStat.GetStat((UpgradeType)i);
            int maxValue = upgradeStat.GetMaxStat((UpgradeType)i);
            row.UpdateProgressBar(statValue, maxValue);
            if (remaingStatPoint == 0)
            {
                row.DisableButton();
            }
        }
    }
}
