using System.Collections.Generic; // C#에서 제공해주는 자료구조(List<T>, dictionary<K, V>같은 값) 사용 가능
using System;
using UnityEngine;


public class Inspector : MonoBehaviour
{
    [Serializable]
    public struct Book // 사용자 정의 타입 / Value 타입 / GC 필요없음 (작은 데이터의 묶음을 자주 할당, 복사하는 개념에서 활용ex) Vector3)
    {
        public string title;
        public string description;
    }

    [Serializable]
    public class Item // 객체를 위한 설계도(속성과 기능) / 유니티에서는 클래스 사용을 권장합니다.
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
    // 직업 : 전사, 도적, 궁수, 마법사
    public Job job = Job.WARRIOR;

    // 인스펙터에 표시되지 않지만, 스크립트에서 사용할 수 있는 변수입니다.
    [HideInInspector]
    public int value = 5;

    [SerializeField] // 유니티에서 비공개(private) 필드를 인스펙터에 노출시키고 유니티의 직렬화 시스템에 포함시킵니다.
    // 사용 목적
    // public -> 노출 + 접근 가능
    // private -> 노출 안됨 + 접근 안됨
    // [SerializeField] -> 노출 + 접근 안됨
    // HideInInspector -> 노출 안됨 + 접근 가능
    // 직렬화(Serialization) : 데이터를 저장하거나 전송하기 위해 특정 형식으로 변환하는 과정입니다.
    // 이 변환을 통해 씬, 프리팹, 스크립트 등에서 객체의 상태를 저장하고 불러올 수 있습니다.

    // 직렬화 조건
    // - public 필드
    // - [SerializeField]가 붙은 private 필드
    // - static 필드가 아닌 인스턴스 필드
    // - 직렬화 가능한 타입이어야 합니다. (예: 기본형, UnityEngine.Object, 사용자 정의 클래스 등)

    // 직렬화 불가한 데이터:
    // 1. Dictionary<TKey, TValue>
    // 2. Interface (예: IEnumerable, IComparable 등)
    // 3. Delegate (예: Action, Func 등)
    // 4. static 키워드가 붙은 필드
    // 5. abstract 클래스나 인터페이스를 구현하지 않은 클래스
    // 

    // 직렬화 가능한 타입 예시:
    // 1. 기본형: int, float, string, bool 등
    // 2. UnityEngine.Object: GameObject, Tramsform, Material, Component, ScriptableObject 등
    // 3. 사용자 정의 클래스: MonoBehaviour, ScriptableObject를 상속받은 클래스
    // 4. 유니티에서 제공해주는 구조체 타입: Vector2, Vector3, Quaternion 등
    // 5. Serializable 특성이 붙은 클래스
    // 6. List<T>와 같은 컬렉션 타입 (단, T는 직렬화 가능한 타입이어야 함)

    //[Space(50)] // 적은 높이만큼 간격이 생깁니다.
    [TextArea(1, 2)] // 여러 줄의 문자열을 입력할 수 있는 필드입니다. (문자열이 장문일 경우 유용합니다.)
    // 기본은 1줄, 설정을 넣으면 그 수치만큼 칸이 늘어납니다.
    public string quest_info;

    public Book book;
    public Item item;

    // 유니티 인스펙터에서는 배열도 리스트로 나오게 됩니다.
    // 리스트<T>는 T 형태의 데이터를 묶음으로 순차적으로 저장할 수 있는 데이터입니다.
    // 데이터의 검색, 추가, 삭제 등의 기능이 제공됩니다.
    public List<Item> items;

    public Book[] books = new Book[5];

    // Range(최소, 최대)를 통해 해당 값을 에디터에서 최소값과 최대가 설정되어있는 스크롤 형태의 값으로 변경됩니다.
    [Range(0, 100)] public int bg = 200;
    [Range(0, 100)] public float sfx = 300;

    private void Start()
    {
        bg = 200;
        Debug.Log(bg);
    }
}
