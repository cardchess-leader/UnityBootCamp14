using System;
using UnityEngine;

public class JsonTester : MonoBehaviour
{
    // ����Ƽ���� ��ü(Object)�� �ʵ�(field)�� json���� ��ȯ�ϱ� ���ؼ���
    // ���������� �޸𸮿��� �����͸� �а� ���� �۾��� �����ؾ� ��.
    // ����, [Serializable] ��Ʈ����Ʈ�� ����Ͽ� �ش� ������ ���� ����ȭ�� ó������ �ʿ䰡 �ֽ��ϴ�.

    // ����ȭ�� �����͸� �����ϰų� �����ϱ� ���� �������� �������� ���·� ��ȯ���ִ� �۾��� �ǹ��մϴ�.

    [Serializable]
    public class Data
    {
        public int hp;
        public int atk;
        public int def;
        public string[] items;
        public Position position;
        public string quest;
        public bool isDead;
        public Data(int hp, int atk, int def, string[] items, Position position, string quest, bool isDead)
        {
            this.hp = hp;
            this.atk = atk;
            this.def = def;
            this.items = items;
            this.position = position;
            this.quest = quest;
            this.isDead = isDead;
        }
    }

    [Serializable]
    public class Position
    {
        public float x;
        public float y;
    }

    public Data myData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var jsonText = Resources.Load<TextAsset>("data01").text;

        if (jsonText == null)
        {
            Debug.LogError("�ش� JSON ������ ���ҽ� �������� ã�� ���߽��ϴ�!");
            return;
        }

        // Json ���ڿ��� ��ü�� ���·� ��ȯ
        myData = JsonUtility.FromJson<Data>(jsonText);

        Debug.Log(myData.hp);
        Debug.Log(myData.atk);
        Debug.Log(myData.def);
        Debug.Log(myData.position.x);
        Debug.Log(myData.position.y);
        Debug.Log(myData.quest);
        Debug.Log(myData.isDead);

        foreach(var item in myData.items)
        {
            Debug.Log(item);
        }
    }
}
