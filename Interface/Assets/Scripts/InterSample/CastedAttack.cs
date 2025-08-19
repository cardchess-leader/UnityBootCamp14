using UnityEngine;

[CreateAssetMenu(menuName = "Attack Strategy/Cast")]
public class CastedAttack : ScriptableObject, IAttackStrategy
{
    public string attackName;

    // ����� ���� ���� �� Ŭ���� ����/�ܺ� ��ο��� �ٷ� ��� ����
    public string AttackName => attackName;

    public int Attack(GameObject self, GameObject target)
    {
        if (target == null)
        {
            Debug.LogWarning("���� ����� �����ϴ�!");
        } else
        {
            Debug.Log($"[Cast Attack: {AttackName}]���� {target.name}�� �����߽��ϴ�!");
            // Ÿ���� ũ�⸦ 20% ���Դϴ�.
            target.transform.localScale *= 0.8f;
        }

        return 0; // ���� ������ �������� ������ �ʽ��ϴ�.
    }
}
