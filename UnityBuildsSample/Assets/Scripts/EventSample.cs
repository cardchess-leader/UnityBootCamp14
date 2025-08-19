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

    private void Start()
    {
        // �̺�Ʈ ����
        onSpaceEnter += DebugOnSpaceEnter;
        // �̺�Ʈ�� �߻����� �� ������ �޼��带 ����մϴ�.
        // += �����ڸ� ����Ͽ� �̺�Ʈ�� �޼��带 �߰��մϴ�.
        // �̺�Ʈ�� �߻��ϸ� �ش� �޼��尡 ȣ��˴ϴ�.
        // DebugOnSpaceEnter �޼���� onSpaceEnter �̺�Ʈ�� �߻��� �� ����˴ϴ�.
    }

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
            // ���⼭ ?�� null ���Ǻ� �������Դϴ�.
            // ��ü�� null�� �ƴ� ���� Invoke �޼��带 ȣ���մϴ�.
            // �޼ҵ�, �Ӽ�, �̺�Ʈ ���� ȣ���� �� null üũ�� �����ϰ� �� �� �ֽ��ϴ�.
            // ���۷��� �Ǵ� nullable Ÿ�Կ� ���˴ϴ�.
            // if (onSpaceEnter != null) ������ �ڵ� ��� ����մϴ�.
            // ���� null�� �� ���ϰ��� null�Դϴ�.

        }
    }

    void DebugOnSpaceEnter(object sender, EventArgs e)
    {
        Debug.Log("<color=yellow>���� Ű �Է� �̺�Ʈ ����</color>");
    }
}
