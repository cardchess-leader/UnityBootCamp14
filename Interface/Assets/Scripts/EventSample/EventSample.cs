using System;
using UnityEngine;

public class EventSample : MonoBehaviour
{
    public event EventHandler onSpaceEnter;
    // �̺�Ʈ ������ �̸��� ���� 'on'���� �����մϴ�.
    // C#���� �������ִ� delegate Ÿ��
    // Eventandler�� ��� ��ġ�� Ŭ�� ���� �̺�Ʈ�� �����ϴ� �뵵
    // �Ű�����
    // Object sender: �̺�Ʈ�� �߻���Ų ��ü�� �ǹ��մϴ�.
    // EventArgs e: �̺�Ʈ�� ���� �߰� ������ �����ϴ� ��ü�� �ǹ��մϴ�.
    // �ش� ���� EventArgs �Ǵ� �ش� �ڽ� Ŭ������ �� �� �ֽ��ϴ�.

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (onSpaceEnter != null)
            {
                onSpaceEnter(this, EventArgs.Empty);
            }
            // this: �̺�Ʈ�� �߻���Ų ��ü(���� Ŭ����)
            // EventArgs.Empty: �̺�Ʈ�� ���� �߰� ������ �ʿ����� ���� �� ����մϴ�.
        }

        // �̺�Ʈ ���� ��� Invoke �Լ��� ����ϴ� ���
        if (Input.GetKeyDown(KeyCode.Space))
        {
            onSpaceEnter?.Invoke(this, EventArgs.Empty);
            // ���� if���� ������ �ǹ��Դϴ�.
            // onSpaceEnter�� null�� �ƴ� ���� Invoke�� �����մϴ�.
        }
    }
}
