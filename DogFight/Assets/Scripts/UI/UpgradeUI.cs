using System.Collections;
using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField]
    UpgradeRow[] upgradeRows = new UpgradeRow[4]; // 0: Armor, 1: Speed, 2: Attack, 3: Ammo

    private void Start()
    {
        StartCoroutine(InitCoroutine());
        Upgrade.Instance.OnLevelUpEvent.AddListener(() =>
        {
            UpdateProgressBars();
        });
    }

    public void OnUpgrade(UpgradeType upgradeType)
    {
        Upgrade.Instance.SpendPoints(upgradeType);
        UpdateProgressBars();
    }

    void UpdateProgressBars()
    {
        Debug.Log("UpdateProgressBars called");
        bool remainingPointsExist = Upgrade.Instance.RemainingPointsExist();
        foreach (UpgradeRow row in upgradeRows)
        {
            int statValue = Upgrade.Instance.GetStat(row.upgradeType);
            row.SetProgressValue(statValue, remainingPointsExist);
        }
    }

    IEnumerator InitCoroutine()
    {
        // Wait until the end of frame to ensure all other Start methods have been called
        yield return null;
        UpdateProgressBars();
    }
}
