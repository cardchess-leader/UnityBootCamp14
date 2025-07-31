using System.Collections.Generic; // C#���� �������ִ� �ڷᱸ��(List<T>, dictionary<K, V>���� ��) ��� ����
using System;
using UnityEngine;


public class Inspector : MonoBehaviour
{
    [Serializable]
    public struct Book // ����� ���� Ÿ�� / Value Ÿ�� / GC �ʿ���� (���� �������� ������ ���� �Ҵ�, �����ϴ� ���信�� Ȱ��ex) Vector3)
    {
        public string title;
        public string description;
    }

    [Serializable]
    public class Item // ��ü�� ���� ���赵(�Ӽ��� ���) / ����Ƽ������ Ŭ���� ����� �����մϴ�.
    {
        public string name;
        public string description;
    }

    public enum Job
    {
        WARRIOR,
        ROGUE,
        ARCHER,
        MAGE
    }
    [Header("- Score -")]
    public int point;
    public int max_point;
    [Header("- Info -")]
    public string nickname;
    // ���� : ����, ����, �ü�, ������
    public Job job = Job.WARRIOR;

    // �ν����Ϳ� ǥ�õ��� ������, ��ũ��Ʈ���� ����� �� �ִ� �����Դϴ�.
    [HideInInspector]
    public int value = 5;

    [SerializeField] // ����Ƽ���� �����(private) �ʵ带 �ν����Ϳ� �����Ű�� ����Ƽ�� ����ȭ �ý��ۿ� ���Խ�ŵ�ϴ�.
    // ��� ����
    // public -> ���� + ���� ����
    // private -> ���� �ȵ� + ���� �ȵ�
    // [SerializeField] -> ���� + ���� �ȵ�
    // HideInInspector -> ���� �ȵ� + ���� ����
    // ����ȭ(Serialization) : �����͸� �����ϰų� �����ϱ� ���� Ư�� �������� ��ȯ�ϴ� �����Դϴ�.
    // �� ��ȯ�� ���� ��, ������, ��ũ��Ʈ ��� ��ü�� ���¸� �����ϰ� �ҷ��� �� �ֽ��ϴ�.

    // ����ȭ ����
    // - public �ʵ�
    // - [SerializeField]�� ���� private �ʵ�
    // - static �ʵ尡 �ƴ� �ν��Ͻ� �ʵ�
    // - ����ȭ ������ Ÿ���̾�� �մϴ�. (��: �⺻��, UnityEngine.Object, ����� ���� Ŭ���� ��)

    // ����ȭ �Ұ��� ������:
    // 1. Dictionary<TKey, TValue>
    // 2. Interface (��: IEnumerable, IComparable ��)
    // 3. Delegate (��: Action, Func ��)
    // 4. static Ű���尡 ���� �ʵ�
    // 5. abstract Ŭ������ �������̽��� �������� ���� Ŭ����
    // 

    // ����ȭ ������ Ÿ�� ����:
    // 1. �⺻��: int, float, string, bool ��
    // 2. UnityEngine.Object: GameObject, Tramsform, Material, Component, ScriptableObject ��
    // 3. ����� ���� Ŭ����: MonoBehaviour, ScriptableObject�� ��ӹ��� Ŭ����
    // 4. ����Ƽ���� �������ִ� ����ü Ÿ��: Vector2, Vector3, Quaternion ��
    // 5. Serializable Ư���� ���� Ŭ����
    // 6. List<T>�� ���� �÷��� Ÿ�� (��, T�� ����ȭ ������ Ÿ���̾�� ��)

    //[Space(50)] // ���� ���̸�ŭ ������ ����ϴ�.
    [TextArea(1, 2)] // ���� ���� ���ڿ��� �Է��� �� �ִ� �ʵ��Դϴ�. (���ڿ��� �幮�� ��� �����մϴ�.)
    // �⺻�� 1��, ������ ������ �� ��ġ��ŭ ĭ�� �þ�ϴ�.
    public string quest_info;

    public Book book;
    public Item item;

    // ����Ƽ �ν����Ϳ����� �迭�� ����Ʈ�� ������ �˴ϴ�.
    // ����Ʈ<T>�� T ������ �����͸� �������� ���������� ������ �� �ִ� �������Դϴ�.
    // �������� �˻�, �߰�, ���� ���� ����� �����˴ϴ�.
    public List<Item> items;

    public Book[] books = new Book[5];

    // Range(�ּ�, �ִ�)�� ���� �ش� ���� �����Ϳ��� �ּҰ��� �ִ밡 �����Ǿ��ִ� ��ũ�� ������ ������ ����˴ϴ�.
    [Range(0, 100)] public int bg = 200;
    [Range(0, 100)] public float sfx = 300;

    private void Start()
    {
        bg = 200;
        Debug.Log(bg);
    }
}
