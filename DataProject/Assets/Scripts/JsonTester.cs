using System;
using UnityEngine;

public class JsonTester : MonoBehaviour
{
    // 유니티에서 객체(Object)의 필드(field)를 json으로 변환하기 위해서는
    // 내부적으로 메모리에서 데이터를 읽고 쓰는 작업이 가능해야 함.
    // 따라서, [Serializable] 어트리뷰트를 사용하여 해당 정보에 대한 직렬화를 처리해줄 필요가 있습니다.

    // 직렬화는 데이터를 저장하거나 전송하기 위해 연속적인 데이터의 형태로 변환해주는 작업을 의미합니다.

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
            Debug.LogError("해당 JSON 파일을 리소스 폴더에서 찾지 못했습니다!");
            return;
        }

        // Json 문자열을 객체의 형태로 변환
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
