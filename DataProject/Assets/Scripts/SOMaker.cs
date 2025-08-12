using UnityEngine;

// 각 데이터별 정리

// JSON
// 외부 텍스트 파일 형태로 저장 관리 가능
// 에디터와 런타임 모두 사용 가능
// 데이터 구조가 자유로운 편
// ex) 세이브 데이터, 설정 데이터, 서버 통신용 데이터 등(DB 연동),
// 수시로 바뀔 수 있는 동적인 데이터

// ScriptableObject
// Unity의 데이터 저장 방식 중 하나
// 에디터에서 데이터 관리가 용이
// 수정사항이 바로 반영되고, 런타임에 빠르게 로드하고 참조도 가능 (메모리 효율 높음)
// 정적인 데이터 구현 (아이템, 퀘스트, 몬스터, 스킬 등)

// PlayerPrefs
// 간단한 데이터 저장에 사용
// Unity의 기본 제공 데이터 저장 방식
// 키-값 쌍으로 저장
// 레지스트리, XML, Plist 등 내부에 저장되는 방식
// 볼륨, 퀘스트 완료 여부, 캐릭터 상태, 환경 설정 등


// 에디터에서 해당 오브젝트 생성 가능
[CreateAssetMenu(fileName = "아이템", menuName = "Item/아이템", order = 1)]
public class SOMaker : ScriptableObject
{
    public enum ItemType
    {
        장비, 소비, 기타
    }

    public string itemName;
    public ItemType itemType;
    public string description;
    public int level;
}
