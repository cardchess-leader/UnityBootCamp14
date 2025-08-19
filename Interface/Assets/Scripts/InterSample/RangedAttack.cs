using UnityEngine;

[CreateAssetMenu(menuName = "Attack Strategy/Range")]
public class RangedAttack : ScriptableObject, IAttackStrategy
{
    public int atk;
    public string attackName;
    public string AttackName => attackName;

    public int Attack(GameObject self, GameObject target)
    {
        Debug.Log($"[Ranged Attack: {AttackName}]���� {target.name}�� �����߽��ϴ�!");
        // Ÿ�ٰ��� �Ÿ��� 1���� ������ �������� �����ϴ�.
        float distance = Vector3.Distance(self.transform.position, target.transform.position);
        if (distance > 1.0f)
        {
            Debug.Log($"Ÿ�ٿ��� {atk}��ŭ �������� �������ϴ�!");
            return atk;
        }
        else
        {
            Debug.Log("Ÿ���� �ʹ� �����̿� �ֽ��ϴ�!");
            return 0;
        }
    }
}
