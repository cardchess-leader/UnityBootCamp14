using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class InterPlayer : MonoBehaviour
{
    // �ν����� ������ ���� ����(���� ������ ���� ����)
    // �ܺο��� ���� �Ұ�(�Ժη� �������� ���ϰ� �ϱ� ����)
    [SerializeField] private List<ScriptableObject> attackList;

    private List<IAttackStrategy> strategyList;

    private void Awake()
    {
        strategyList = attackList.Cast<IAttackStrategy>().ToList();
    }

    public void ActionPerformed(int index) // �� ���� ����(melee, ranged, casted)�� Ÿ�ٿ��� �����մϴ�.
    {
        // Ÿ���� tag�� ã���ϴ�.
        GameObject target = GameObject.FindGameObjectWithTag("Enemy");
        if (index < 0 || index >= strategyList.Count)
        {
            Debug.LogWarning("��ȿ���� ���� �����Դϴ�!");
            return;
        }
        int damage = strategyList[index].Attack(gameObject, target);
        // ���� �� Ÿ���� �ִٸ� �������� �����ϴ�.
        target.GetComponent<ITakeDamage>()?.TakeDamage(damage);
    }
}
