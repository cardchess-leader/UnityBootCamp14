using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class InterPlayer : MonoBehaviour
{
    // 인스펙터 내에서 접근 가능(내부 데이터 연결 목적)
    // 외부에서 접근 불가(함부로 접근하지 못하게 하기 위함)
    [SerializeField] private List<ScriptableObject> attackList;

    private List<IAttackStrategy> strategyList;

    private void Awake()
    {
        strategyList = attackList.Cast<IAttackStrategy>().ToList();
    }

    public void ActionPerformed(int index) // 각 공격 전략(melee, ranged, casted)를 타겟에게 실행합니다.
    {
        // 타겟을 tag로 찾습니다.
        GameObject target = GameObject.FindGameObjectWithTag("Enemy");
        if (index < 0 || index >= strategyList.Count)
        {
            Debug.LogWarning("유효하지 않은 공격입니다!");
            return;
        }
        int damage = strategyList[index].Attack(gameObject, target);
        // 공격 후 타겟이 있다면 데미지를 입힙니다.
        target.GetComponent<ITakeDamage>()?.TakeDamage(damage);
    }
}
