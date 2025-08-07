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
    public UnitInventory inventory; // ������ �κ��丮
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
        return false; // �ִ� ������ �����Ͽ� ���׷��̵� ����
    }
}
