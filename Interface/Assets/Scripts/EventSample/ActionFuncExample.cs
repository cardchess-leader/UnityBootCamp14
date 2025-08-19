using System;
using UnityEngine;

public class ActionFuncExample : MonoBehaviour
{
    // Action<T>�� �ִ� 16���� �Ű������� ���� �� �ִ� �븮��(delegate)�Դϴ�.
    // ��ȯ���� ���� �޼ҵ带 ������ �� �ֽ��ϴ�.
    // ������ �����̸�, ����� ���� �ʴ� ����

    public Action<int> myAction;
    public Action<int, string> myAction2;

    // Func�� �������� ������ �κ��� Func�� ����� �Լ��� ��ȯ Ÿ���Դϴ�.
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
            Debug.Log("������ �����մϴ�.");
        }
        else
        {
            Debug.Log("������ �� �����ϴ�.");
        }

        func2 += s => int.Parse(s);
    }

    void Rage(int value)
    {
        Debug.Log($"�г�� ���� ���ݷ��� <color=red>{value}</color> �����߽��ϴ�.");
    }

    void Heal(int value, string character)
    {
        Debug.Log($"ȸ�� �������� <color=yellow>{character}</color>�� ü���� <color=green>{value}</color> ȸ���Ǿ����ϴ�.");
    }

    bool AttackAble()
    {
        int rand = UnityEngine.Random.Range(0, 10);
        return rand <= 3;
    }

    int result(string s) => int.Parse(s);
}
