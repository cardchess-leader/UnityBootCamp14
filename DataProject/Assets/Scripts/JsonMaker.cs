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
        // 1) 설계한 객체에 대한 초기화 작업
        QuestList questList = new QuestList()
        {
            quests = new QuestData[]
            {
                new QuestData
                {
                    questName = "시작이 반이다",
                    reward = "exp + 100",
                    description = "시작이라도 하면 반이라도 가지."
                },
                new QuestData
                {
                    questName = "중간만 해라",
                    reward = "exp + 150",
                    description = "중간이라도 하는게 어디야."
                },
                new QuestData
                {
                    questName = "할거면 끝까지 해라",
                    reward = "exp + 500",
                    description = "그냥 다하라는거잖아"
                },
                new QuestData
                {
                    questName = "123",
                    reward = "33",
                    description = "fffffff"
                }
            }
        };
        // 2) Json 문자열로 변환 (JsonUtility.ToJson(Object obj, bool prettyPrint = false))
        string json = JsonUtility.ToJson(questList, true);
        // ToJson 메서드의 두 번째 인자는 pretty print 여부를 결정합니다.
        // true로 설정하면 가독성이 좋은 형태로 변환됩니다.
        // false로 설정하면 압축된 형태로 변환됩니다.

        Debug.Log("JSON IS: " + json);

        // 3) 저장 경로에 대한 작성을 진행합니다.
        string path = Path.Combine(Application.persistentDataPath, "quests.json");
        Debug.Log("JSON 저장 경로: " + path);
        // Path.Combine(string path1, string path2)
        // 두 개의 경로를 결합하여 하나의 경로를 생성합니다.
        // 저장 위치/파일명으로 자주 사용됩니다.

        // Application.persistentDataPath : 유니티 애플리케이션의 지속적인 데이터 저장 경로를 반환합니다.
        // 이 경로는 플랫폼에 따라 다르게 설정되며, 일반적으로 사용자 데이터나 설정 파일을 저장하는 데 사용됩니다.
        // 만약 이 경로가 존재하지 않는다면, 유니티는 자동으로 해당 경로를 생성합니다.

        // 4) Json 문자열을 파일로 저장
        File.WriteAllText(path, json);

        Debug.Log("JSON 파일이 성공적으로 저장되었습니다!");

        // ============== 파일 로드 =============== //
        // 1) 경로에 파일이 존재하는지 확인
        if (File.Exists(path))
        {
            string json2 = File.ReadAllText(path);
            QuestList loaded = JsonUtility.FromJson<QuestList>(json2);
            Debug.Log($"퀘스트 수락 : {loaded.quests[0].questName}");
        }
        else
        {
            Debug.LogWarning("저장된 JSON 파일이 존재하지 않습니다!");
        }
    }
}
