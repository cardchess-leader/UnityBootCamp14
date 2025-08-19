using UnityEngine;
using UnityEngine.Events;

// C#의 event와의 차이점

//                                      UnityAction      vs      UnityEvent
// 타입:                                  delegate       vs      클래스
// 기능:                                  함수 참조      vs      에디터에서 핸들러 직접 등록 가능
// 사용할 상황                    스크립트 코드 내 처리          인스팩터용 이벤트 시스템
// 속도:                            빠름(직접 호출)              느림(Invoke 메서드 사용)(연결 정보에 대한 파싱 후 런타임 실행 방식)
// 메모리:                          적음(함수 참조)              많음(클래스 인스턴스 생성)
// GC 발생 여부:                    없음(직접 호출)              있음(Invoke 메서드 사용)
// 편의성:                  낮음(직접 코드 작성 필요)            높음(인스팩터에서 핸들러 등록 가능)

// 사용할 수 있는 선택지
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
        Debug.Log("color=cyan>Sample 실행 </color>");
    }

    private void Sample2()
    {
        Debug.Log("color=blue>Sample2 실행 </color>");

    }
}

// 이 다양한 delegate들을
// 언제 어떤걸 사용해야하는가? 

// 고성능을 원한다 -> C# delegate
// 콜백을 원한다 -> Action, UnityAction
// 인스펙터에서 핸들러를 등록하고 싶다 -> UnityEvent
// 이벤트 시그니처가 필요하다 -> Func<T>, Action<T>, EventHandler<T>
// 시그니처란: 1. 함수의 이름, 2. 매개변수의 타입과 개수, 3. 반환 타입을 포함하는 함수의 형태를 의미합니다.

