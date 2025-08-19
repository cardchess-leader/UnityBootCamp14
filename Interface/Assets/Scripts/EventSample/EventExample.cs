using System;
using UnityEngine;

// event : 특정 상황이 발생했을 때 알림을 보내는 메커니즘입니다.
// 1. 플레이어가 죽었을 때 -> 알림 전달 -> 메소드 호출

//                        Action             vs               event Action
// 외부 호출           : 가능                vs               불가능
// 외부 대입           : 가능                vs               불가능
// null 체크           : 불가능              vs               가능
// 구독 (subscribe)    : 불가능              vs               가능
// 해제 (unsubscribe)  : 불가능              vs               가능
// 주 용도             : 메소드 참조         vs               이벤트 알림 시스템

//public class Tester : MonoBehaviour
//    {
//    // Action은 메소드를 참조하는 대리자(delegate)입니다.
//    // Action은 매개변수가 없고 반환값이 없는 메소드를 참조할 수 있습니다.
//    Action onDeath;
//    private void Start()
//    {
//        EventExample eventExample = new EventExample();
//        // 액션에 메소드를 등록합니다.
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

// event Action과 Action의 차이점
// Action은 단순히 메소드를 참조하는 대리자(delegate)입니다.
// event Action은 Invoke를 통해서 호출이 가능합니다.
// event는 외부에서 직접 호출할 수 없고, 오직 해당 클래스 내부에서만 호출할 수 있습니다.
// event는 구독(subscribe)과 해제(unsubscribe)를 통해 메소드를 등록하고 제거할 수 있는 기능을 제공합니다.
// 또한 event는 외부에서 직접 대입할 수 없고, +=와 -= 연산자를 통해서만 메소드를 등록하거나 제거할 수 있습니다.
// 또한 event는 null 체크를 자동으로 수행하여, 등록된 메소드가 없을 때 예외를 방지합니다.
public class EventExample : MonoBehaviour
{
    public Action onDeath;
    public event Action onStart;

    private void Start()
    {
        // 액션은 +=를 통해 함수(메소드)를 액션에 등록할 수 있습니다.
        // 액션의 기능을 호출하면 등록되어있는 함수가 순차적으로 호출됩니다.
        onStart += Ready;
        onStart += Fight;

        onDeath += Damaged;
        onDeath += Dead;

        onStart?.Invoke(); // onStart에 등록된 함수들을 호출합니다.
        onDeath?.Invoke(); // onDeath에 등록된 함수들을 호출합니다.

        // 함수처럼 호출하는 것도 가능합니다.
        onStart();
        onDeath();

        // onStart에 등록된 모든 함수들을 제거하는 방법:
        onStart = null; // onStart에 등록된 함수들을 모두 제거합니다.
        onDeath = null;
        

        //onStart = null; // onStart에 등록된 함수들을 모두 제거합니다.
        //onStart(); // 오류가 발생하지 않습니다. (null 참조 예외가 발생하지 않습니다.) 따라서 onStart?.Invoke()와 동일합니다.

        // 둘의 차이? 없음. 문법 스타일 차이
        // Invoke 방식이면 null 체크 가능, 외부에서의 호출, 안전성 요구 시 추천
        // 함수 형태면 따로 설계, 내부 코드이거나 단순 호출일 경우 해당 방식 추천
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