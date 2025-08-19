using UnityEngine;

[CreateAssetMenu(menuName = "Attack Strategy/Range")]
public class RangedAttack : ScriptableObject, IAttackStrategy
{
    public int atk;
    public string attackName;
    public string AttackName => attackName;

    public int Attack(GameObject self, GameObject target)
    {
        Debug.Log($"[Ranged Attack: {AttackName}]으로 {target.name}을 공격했습니다!");
        // 타겟과의 거리가 1보다 작으면 데미지를 입힙니다.
        float distance = Vector3.Distance(self.transform.position, target.transform.position);
        if (distance > 1.0f)
        {
            Debug.Log($"타겟에게 {atk}만큼 데미지를 입혔습니다!");
            return atk;
        }
        else
        {
            Debug.Log("타겟이 너무 가까이에 있습니다!");
            return 0;
        }
    }
}
