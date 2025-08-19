using System;
using UnityEngine;

public class ActionFuncExample : MonoBehaviour
{
    // Action<T>는 최대 16개의 매개변수를 가질 수 있는 대리자(delegate)입니다.
    // 반환값이 없는 메소드를 참조할 수 있습니다.
    // 전달이 목적이며, 결과는 받지 않는 형태

    public Action<int> myAction;
    public Action<int, string> myAction2;

    // Func는 마지막에 적히는 부분이 Func가 사용할 함수의 반환 타입입니다.
    public Func<bool> func1;
    public Func<string, int> func2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myAction += Rage;
        myAction2 += Heal;
        myAction(10);
        myAction2(20, "Steve");

        func1 += AttackAble;
        if (func1())
        {
            Debug.Log("공격이 가능합니다.");
        }
        else
        {
            Debug.Log("공격할 수 없습니다.");
        }

        func2 += s => int.Parse(s);
    }

    void Rage(int value)
    {
        Debug.Log($"분노로 인해 공격력이 <color=red>{value}</color> 증가했습니다.");
    }

    void Heal(int value, string character)
    {
        Debug.Log($"회복 마법으로 <color=yellow>{character}</color>의 체력이 <color=green>{value}</color> 회복되었습니다.");
    }

    bool AttackAble()
    {
        int rand = UnityEngine.Random.Range(0, 10);
        return rand <= 3;
    }

    int result(string s) => int.Parse(s);
}
