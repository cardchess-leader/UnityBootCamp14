using UnityEngine;
using System.Collections;

public class IEnumeratorSample : MonoBehaviour
{
    class CustomCollection : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };
            foreach (int number in numbers)
            {
                yield return number;
            }   
        }
    }


    int[] numbers = { 1, 2, 3, 4, 5 };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IEnumerator enumerator = numbers.GetEnumerator();

        while (enumerator.MoveNext())
        {
            int number = (int)enumerator.Current;
            Debug.Log("Current number: " + number);
        }

        CustomCollection customCollection = new CustomCollection();
        // foreach�� ��ȯ ������ �����͸� �ڵ����� ��ȸ�մϴ�.
        foreach (int number in customCollection)
        {
            Debug.Log("Number from CustomCollection: " + number);
        }
    
        foreach(var message in GetMessage())
        {
            Debug.Log("�޽���: " + message);
        }
    }

    // yield�� C#���� �� ���� �ϳ��� ���� �ѱ��, �޼ҵ尡 �Ͻ� �����Ǹ� �ļ� ������ ���������� ��ȯ�ϰ� �մϴ�.
    // yield�� yield return, yield break�� ���˴ϴ�.

    // yield return�� ���� ��ȯ�ϰ� �޼ҵ带 �Ͻ� �����մϴ�.
    // ȣ���ڰ� ���� ���� ��û�� �� �޼ҵ�� ���� ���¿��� ��� ����˴ϴ�.

    // yield break�� ��ȯ �۾��� �����մϴ�. �� �̻� ���� ��ȯ���� �ʽ��ϴ�.
    // �÷����� �ڵ����� ��ȸ�ϴ� foreach�� ���� ���˴ϴ�.

    // ����: ���� �ʿ�� �� ������ ����� �̷�� ���(�޸� ȿ���� ����, ū �����͸� ó���� �� ������ Ů�ϴ�.)
    // -> ��� �����͸� �����ϴ°� �ƴ� �ʿ��� �κи� ó���� �� �ְ� �Ǳ� ����

    // IEnumerable: �ݺ� ������ �÷����� ��Ÿ���� �������̽��Դϴ�.
    // �� ����� ������ Ŭ������ �ݺ��� �� �ִ� ��ü�� �Ǹ� foreach ��� �������� Ž���� ������ �� �ְ� �˴ϴ�.
    // �ش� �������̽��� �����ϱ� ���ؼ��� GetEnumerator() �޼ҵ带 �����ؾ� �մϴ�.

    // IEnumerator: �ݺ� ������ �÷����� ���� ��ġ�� ��Ÿ���� �������̽��Դϴ�.
    // �� �������̽��� MoveNext() �޼ҵ�� Current �Ӽ��� �����մϴ�.
    // MoveNext() �޼ҵ�� �÷����� ���� ��ҷ� �̵��ϰ�, Current �Ӽ��� ���� ��Ҹ� ��ȯ�մϴ�.
    // Current�� ���ؼ� ���� �� Ȯ��
    // Reset()�� ���ؼ� ���� ��� ó��

    static IEnumerable GetMessage() {
        Debug.Log("�޼ҵ� ����");
        yield return "��";
        Debug.Log("Ż�� �� ���ƿ� 1");
        yield return "ȣ";
        Debug.Log("Ż�� �� ���ƿ� 2");
        yield break; // ��ȯ �۾��� �����մϴ�.
        // Unreachable code
        Debug.Log("�� �ڵ�� ������� �ʽ��ϴ�.");
        yield return 3;
    }
}
