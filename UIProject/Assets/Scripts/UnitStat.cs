using UnityEngine;

// 유닛의 능력치
public class UnitStat : MonoBehaviour
{
    readonly string[] upgradeRequirements = new string[]
    {
        "100골드",
        "100골드 + 루비",
        "200골드 + 사파이어 + 마력석",
        "최대 강화 완료",
    };
    public UnitInventory inventory; // 유닛의 인벤토리
    public int currUpgrade = 0;
    int max_level => upgradeRequirements.Length - 1;
    public bool isMaxUpgrade => currUpgrade == max_level;
    public string upgradeRequirement => upgradeRequirements[currUpgrade];

    public bool TryUpgrade()
    {
        if (currUpgrade < max_level)
        {
            if (inventory.HasRequiredItems(upgradeRequirements[currUpgrade]))
            {
                currUpgrade++;
                return true;
            }
        }
        return false; // 최대 레벨에 도달하여 업그레이드 실패
    }
}
