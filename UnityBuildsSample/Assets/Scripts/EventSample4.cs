using System;
using UnityEngine;

public class DamageEventArgs : EventArgs
{
    // ������ ���� ���� ����(������Ƽ�� �۾��ϸ�, get��ɸ� �ַ� Ȱ��ȭ �մϴ�.)
    public int Value { get; } // Value�� ���� ���ٸ� ����
    public string EventName { get; }

    public DamageEventArgs(int value, string eventName)
    {
        Value = value; // �����ڿ��� Value�� �ʱ�ȭ
        EventName = eventName; // �����ڿ��� EventName�� �ʱ�ȭ
    }
}

public class EventSample4 : MonoBehaviour
{
    public event EventHandler<DamageEventArgs> OnDamaged; // �������� �޾��� ���� ���� �̺�Ʈ �ڵ鷯

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư Ŭ�� ��
        {
            TakeDamage(UnityEngine.Random.Range(10, 200), "���� ����"); // �������� �޾Ҵٰ� �̺�Ʈ �߻�
        }
    }

    public void TakeDamage(int damage, string eventName)
    {
        OnDamaged?.Invoke(this, new DamageEventArgs(damage, eventName));

        Debug.Log($"<color=red>[{eventName}] �÷��̾ {damage} �������� �޾ҽ��ϴ�. </color>");
    }
}
