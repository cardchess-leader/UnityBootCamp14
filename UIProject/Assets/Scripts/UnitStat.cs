using UnityEngine;

// ������ �ɷ�ġ
public class UnitStat : MonoBehaviour
{
    readonly string[] upgradeRequirements = new string[]
    {
        "100���",
        "100��� + ���",
        "200��� + �����̾� + ���¼�",
        "�ִ� ��ȭ �Ϸ�",
    };
    readonly string[] wizardStats = new string[]
    {
            "ATK: 10, INT: 10, DEF: 10",
            "ATK: 20, INT: 20, DEF: 20",
            "ATK: 30, INT: 30, DEF: 30",
            "ATK: 40, INT: 40, DEF: 40",
    };
    public UnitInventory inventory; // ������ �κ��丮
    public int currUpgrade = 0;
    int max_level => upgradeRequirements.Length - 1;
    public bool isMaxUpgrade => currUpgrade == max_level;
    public string upgradeRequirement => upgradeRequirements[currUpgrade];
    public string wizardStat => wizardStats[currUpgrade];

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
        return false; // �ִ� ������ �����Ͽ� ���׷��̵� ����
    }
}
