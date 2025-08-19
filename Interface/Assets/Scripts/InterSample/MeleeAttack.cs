using UnityEngine;

[CreateAssetMenu(menuName = "Attack Strategy/Melee")]
public class MeleeAttack : ScriptableObject, IAttackStrategy
{
    public int atk;
    public string attackName;

    // 명시적 구현 제거 → 클래스 내부/외부 모두에서 바로 사용 가능
    public string AttackName => attackName;

    public int Attack(GameObject self, GameObject target)
    {
        Debug.Log($"[Melee Attack: {AttackName}]으로 {target.name}을 공격했습니다!");
        // 타겟과의 거리가 1보다 작으면 데미지를 입힙니다.
        float distance = Vector3.Distance(self.transform.position, target.transform.position);
        if (distance < 1.1f)
        {
            Debug.Log($"타겟에게 {atk}만큼 데미지를 입혔습니다!");
            return atk;
        }
        else
        {
            Debug.Log("타겟이 너무 멀리 떨어져 있습니다!");
            return 0;
        }
    }
}
