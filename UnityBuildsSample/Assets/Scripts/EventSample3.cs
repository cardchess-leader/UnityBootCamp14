using UnityEngine;
using UnityEngine.Events;

// C#�� event���� ������

//                                      UnityAction      vs      UnityEvent
// Ÿ��:                                  delegate       vs      Ŭ����
// ���:                                  �Լ� ����      vs      �����Ϳ��� �ڵ鷯 ���� ��� ����
// ����� ��Ȳ                    ��ũ��Ʈ �ڵ� �� ó��          �ν����Ϳ� �̺�Ʈ �ý���
// �ӵ�:                            ����(���� ȣ��)              ����(Invoke �޼��� ���)(���� ������ ���� �Ľ� �� ��Ÿ�� ���� ���)
// �޸�:                          ����(�Լ� ����)              ����(Ŭ���� �ν��Ͻ� ����)
// GC �߻� ����:                    ����(���� ȣ��)              ����(Invoke �޼��� ���)
// ���Ǽ�:                  ����(���� �ڵ� �ۼ� �ʿ�)            ����(�ν����Ϳ��� �ڵ鷯 ��� ����)

// ����� �� �ִ� ������
// 1. C# delegate
// 2. UnityAction
// 3. C# Func<T>, Action<T>, EventHandler<T>

public class EventSample3 : MonoBehaviour
{
    public UnityEvent OnKButtonEnter;
    public UnityAction OnAction;

    private void Start()
    {
        OnKButtonEnter.AddListener(Sample);
        OnAction += Sample2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            OnKButtonEnter?.Invoke();
        }
    }

    private void Sample()
    {
        Debug.Log("color=cyan>Sample ���� </color>");
    }

    private void Sample2()
    {
        Debug.Log("color=blue>Sample2 ���� </color>");

    }
}

// �� �پ��� delegate����
// ���� ��� ����ؾ��ϴ°�? 

// ������ ���Ѵ� -> C# delegate
// �ݹ��� ���Ѵ� -> Action, UnityAction
// �ν����Ϳ��� �ڵ鷯�� ����ϰ� �ʹ� -> UnityEvent
// �̺�Ʈ �ñ״�ó�� �ʿ��ϴ� -> Func<T>, Action<T>, EventHandler<T>
// �ñ״�ó��: 1. �Լ��� �̸�, 2. �Ű������� Ÿ�԰� ����, 3. ��ȯ Ÿ���� �����ϴ� �Լ��� ���¸� �ǹ��մϴ�.

