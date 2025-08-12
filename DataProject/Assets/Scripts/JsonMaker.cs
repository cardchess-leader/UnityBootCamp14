using System;
using System.IO;
using UnityEngine;

public class JsonMaker : MonoBehaviour
{
    [Serializable]
    public class QuestData
    {
        public string questName;
        public string reward;
        public string description;

    }

    [Serializable]
    public class QuestList
    {
        public QuestData[] quests;
    }


    private void Start()
    {
        // 1) ������ ��ü�� ���� �ʱ�ȭ �۾�
        QuestList questList = new QuestList()
        {
            quests = new QuestData[]
            {
                new QuestData
                {
                    questName = "������ ���̴�",
                    reward = "exp + 100",
                    description = "�����̶� �ϸ� ���̶� ����."
                },
                new QuestData
                {
                    questName = "�߰��� �ض�",
                    reward = "exp + 150",
                    description = "�߰��̶� �ϴ°� ����."
                },
                new QuestData
                {
                    questName = "�ҰŸ� ������ �ض�",
                    reward = "exp + 500",
                    description = "�׳� ���϶�°��ݾ�"
                },
                new QuestData
                {
                    questName = "123",
                    reward = "33",
                    description = "fffffff"
                }
            }
        };
        // 2) Json ���ڿ��� ��ȯ (JsonUtility.ToJson(Object obj, bool prettyPrint = false))
        string json = JsonUtility.ToJson(questList, true);
        // ToJson �޼����� �� ��° ���ڴ� pretty print ���θ� �����մϴ�.
        // true�� �����ϸ� �������� ���� ���·� ��ȯ�˴ϴ�.
        // false�� �����ϸ� ����� ���·� ��ȯ�˴ϴ�.

        Debug.Log("JSON IS: " + json);

        // 3) ���� ��ο� ���� �ۼ��� �����մϴ�.
        string path = Path.Combine(Application.persistentDataPath, "quests.json");
        Debug.Log("JSON ���� ���: " + path);
        // Path.Combine(string path1, string path2)
        // �� ���� ��θ� �����Ͽ� �ϳ��� ��θ� �����մϴ�.
        // ���� ��ġ/���ϸ����� ���� ���˴ϴ�.

        // Application.persistentDataPath : ����Ƽ ���ø����̼��� �������� ������ ���� ��θ� ��ȯ�մϴ�.
        // �� ��δ� �÷����� ���� �ٸ��� �����Ǹ�, �Ϲ������� ����� �����ͳ� ���� ������ �����ϴ� �� ���˴ϴ�.
        // ���� �� ��ΰ� �������� �ʴ´ٸ�, ����Ƽ�� �ڵ����� �ش� ��θ� �����մϴ�.

        // 4) Json ���ڿ��� ���Ϸ� ����
        File.WriteAllText(path, json);

        Debug.Log("JSON ������ ���������� ����Ǿ����ϴ�!");

        // ============== ���� �ε� =============== //
        // 1) ��ο� ������ �����ϴ��� Ȯ��
        if (File.Exists(path))
        {
            string json2 = File.ReadAllText(path);
            QuestList loaded = JsonUtility.FromJson<QuestList>(json2);
            Debug.Log($"����Ʈ ���� : {loaded.quests[0].questName}");
        }
        else
        {
            Debug.LogWarning("����� JSON ������ �������� �ʽ��ϴ�!");
        }
    }
}
