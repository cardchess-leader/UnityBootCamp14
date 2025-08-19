using System;
using UnityEngine;

// event : Ư�� ��Ȳ�� �߻����� �� �˸��� ������ ��Ŀ�����Դϴ�.
// 1. �÷��̾ �׾��� �� -> �˸� ���� -> �޼ҵ� ȣ��

//                        Action             vs               event Action
// �ܺ� ȣ��           : ����                vs               �Ұ���
// �ܺ� ����           : ����                vs               �Ұ���
// null üũ           : �Ұ���              vs               ����
// ���� (subscribe)    : �Ұ���              vs               ����
// ���� (unsubscribe)  : �Ұ���              vs               ����
// �� �뵵             : �޼ҵ� ����         vs               �̺�Ʈ �˸� �ý���

//public class Tester : MonoBehaviour
//    {
//    // Action�� �޼ҵ带 �����ϴ� �븮��(delegate)�Դϴ�.
//    // Action�� �Ű������� ���� ��ȯ���� ���� �޼ҵ带 ������ �� �ֽ��ϴ�.
//    Action onDeath;
//    private void Start()
//    {
//        EventExample eventExample = new EventExample();
//        // �׼ǿ� �޼ҵ带 ����մϴ�.
//        eventExample.onDeath += Damaged;
//        eventExample.onStart += Dead;
//    }
//    private void Damaged()
//    {
//        Debug.Log("<color=red><b>Critical Damage!.</b></color>");
//    }
//    private void Dead()
//    {
//        Debug.Log("<color=blue><b>A Hero is fallen.</b></color>");
//    }
//}

// event Action�� Action�� ������
// Action�� �ܼ��� �޼ҵ带 �����ϴ� �븮��(delegate)�Դϴ�.
// event Action�� Invoke�� ���ؼ� ȣ���� �����մϴ�.
// event�� �ܺο��� ���� ȣ���� �� ����, ���� �ش� Ŭ���� ���ο����� ȣ���� �� �ֽ��ϴ�.
// event�� ����(subscribe)�� ����(unsubscribe)�� ���� �޼ҵ带 ����ϰ� ������ �� �ִ� ����� �����մϴ�.
// ���� event�� �ܺο��� ���� ������ �� ����, +=�� -= �����ڸ� ���ؼ��� �޼ҵ带 ����ϰų� ������ �� �ֽ��ϴ�.
// ���� event�� null üũ�� �ڵ����� �����Ͽ�, ��ϵ� �޼ҵ尡 ���� �� ���ܸ� �����մϴ�.
public class EventExample : MonoBehaviour
{
    public Action onDeath;
    public event Action onStart;

    private void Start()
    {
        // �׼��� +=�� ���� �Լ�(�޼ҵ�)�� �׼ǿ� ����� �� �ֽ��ϴ�.
        // �׼��� ����� ȣ���ϸ� ��ϵǾ��ִ� �Լ��� ���������� ȣ��˴ϴ�.
        onStart += Ready;
        onStart += Fight;

        onDeath += Damaged;
        onDeath += Dead;

        onStart?.Invoke(); // onStart�� ��ϵ� �Լ����� ȣ���մϴ�.
        onDeath?.Invoke(); // onDeath�� ��ϵ� �Լ����� ȣ���մϴ�.

        // �Լ�ó�� ȣ���ϴ� �͵� �����մϴ�.
        onStart();
        onDeath();

        // onStart�� ��ϵ� ��� �Լ����� �����ϴ� ���:
        onStart = null; // onStart�� ��ϵ� �Լ����� ��� �����մϴ�.
        onDeath = null;
        

        //onStart = null; // onStart�� ��ϵ� �Լ����� ��� �����մϴ�.
        //onStart(); // ������ �߻����� �ʽ��ϴ�. (null ���� ���ܰ� �߻����� �ʽ��ϴ�.) ���� onStart?.Invoke()�� �����մϴ�.

        // ���� ����? ����. ���� ��Ÿ�� ����
        // Invoke ����̸� null üũ ����, �ܺο����� ȣ��, ������ �䱸 �� ��õ
        // �Լ� ���¸� ���� ����, ���� �ڵ��̰ų� �ܼ� ȣ���� ��� �ش� ��� ��õ
    }

    private void Ready()
    {

    }

    private void Fight()
    {
        Debug.Log("<color=green><b>Fight!</b></color>");;
    }

    private void Damaged()
    {
        Debug.Log("<color=red><b>Critical Damage!.</b></color>");
    }

    private void Dead()
    {
        Debug.Log("<color=blue><b>A Hero is fallen.</b></color>");
    }
}