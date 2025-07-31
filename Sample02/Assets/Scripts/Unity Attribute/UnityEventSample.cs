using UnityEngine;
using UnityEngine.Events;

//[ExecuteInEditMode]
public class UnityEventSample : MonoBehaviour
{
    // ������ �ν����Ϳ��� �ʵ� ���� ���콺�� �÷��� �� ������ �����ִ� ���
    [Tooltip("�̺�Ʈ ����Ʈ�� �߰��ϰ�, ������ ����� ���� ���� ������Ʈ�� ����ϼ���.")]
    public UnityEvent action;

    private void Update()
    {
        action.Invoke(); // �׼ǿ� ��ϵ� �Լ��� �����մϴ�.
    }

    public void Move()
    {
        gameObject.transform.Translate(0, 0.1f, 0);
    }
}
