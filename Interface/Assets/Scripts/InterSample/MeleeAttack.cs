using UnityEngine;

[CreateAssetMenu(menuName = "Attack Strategy/Melee")]
public class MeleeAttack : ScriptableObject, IAttackStrategy
{
    public int atk;
    public string attackName;

    // ����� ���� ���� �� Ŭ���� ����/�ܺ� ��ο��� �ٷ� ��� ����
    public string AttackName => attackName;

    public int Attack(GameObject self, GameObject target)
    {
        Debug.Log($"[Melee Attack: {AttackName}]���� {target.name}�� �����߽��ϴ�!");
        // Ÿ�ٰ��� �Ÿ��� 1���� ������ �������� �����ϴ�.
        float distance = Vector3.Distance(self.transform.position, target.transform.position);
        if (distance < 1.1f)
        {
            Debug.Log($"Ÿ�ٿ��� {atk}��ŭ �������� �������ϴ�!");
            return atk;
        }
        else
        {
            Debug.Log("Ÿ���� �ʹ� �ָ� ������ �ֽ��ϴ�!");
            return 0;
        }
    }
}
