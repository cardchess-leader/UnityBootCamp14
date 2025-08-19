using UnityEngine;

[CreateAssetMenu(menuName = "Attack Strategy/Cast")]
public class CastedAttack : ScriptableObject, IAttackStrategy
{
    public string attackName;

    // 명시적 구현 제거 → 클래스 내부/외부 모두에서 바로 사용 가능
    public string AttackName => attackName;

    public int Attack(GameObject self, GameObject target)
    {
        if (target == null)
        {
            Debug.LogWarning("공격 대상이 없습니다!");
        } else
        {
            Debug.Log($"[Cast Attack: {AttackName}]으로 {target.name}을 공격했습니다!");
            // 타겟의 크기를 20% 줄입니다.
            target.transform.localScale *= 0.8f;
        }

        return 0; // 마법 공격은 데미지를 입히지 않습니다.
    }
}
