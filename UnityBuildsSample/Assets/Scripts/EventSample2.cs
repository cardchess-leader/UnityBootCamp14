using System;
using UnityEngine;

// EventSample�� ������Ʈ�� �پ��ִ� ��ü�� �������ְڽ��ϴ�.
public class EventSample2 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // �ٸ� Ŭ�������� �̺�Ʈ�� �����ϴ� ���

        // Ŭ���� ����
        EventSample eventSample = GetComponent<EventSample>();

        // Ŭ������ ���� �̺�Ʈ�� �߰�
        eventSample.onSpaceEnter += OnSpaceButton;
    }

    private void OnSpaceButton(object sender, EventArgs e)
    {
        Debug.Log("<color=blue>Sample2���� ����� ���!</color>");
    }
}
