using System;
using UnityEngine;

public class EventSample : MonoBehaviour
{
    public event EventHandler onSpaceEnter;
    // 이벤트 변수의 이름은 보통 'on'으로 시작합니다.
    // C#에서 제공해주는 delegate 타입
    // Eventandler의 경우 터치나 클릭 등의 이벤트를 관찰하는 용도
    // 매개변수
    // Object sender: 이벤트를 발생시킨 객체를 의미합니다.
    // EventArgs e: 이벤트에 대한 추가 정보를 제공하는 객체를 의미합니다.
    // 해당 값은 EventArgs 또는 해당 자식 클래스가 들어갈 수 있습니다.

    private void Start()
    {
        // 이벤트 구독
        onSpaceEnter += DebugOnSpaceEnter;
        // 이벤트가 발생했을 때 실행할 메서드를 등록합니다.
        // += 연산자를 사용하여 이벤트에 메서드를 추가합니다.
        // 이벤트가 발생하면 해당 메서드가 호출됩니다.
        // DebugOnSpaceEnter 메서드는 onSpaceEnter 이벤트가 발생할 때 실행됩니다.
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (onSpaceEnter != null)
            {
                onSpaceEnter(this, EventArgs.Empty);
            }
            // this: 이벤트를 발생시킨 객체(현재 클래스)
            // EventArgs.Empty: 이벤트에 대한 추가 정보가 필요하지 않을 때 사용합니다.
        }

        // 이벤트 실행 방식 Invoke 함수를 사용하는 방식
        if (Input.GetKeyDown(KeyCode.Space))
        {
            onSpaceEnter?.Invoke(this, EventArgs.Empty);
            // 위의 if문과 동일한 의미입니다.
            // onSpaceEnter가 null이 아닐 때만 Invoke를 실행합니다.
            // 여기서 ?는 null 조건부 연산자입니다.
            // 객체가 null이 아닐 때만 Invoke 메서드를 호출합니다.
            // 메소드, 속성, 이벤트 등을 호출할 때 null 체크를 간편하게 할 수 있습니다.
            // 레퍼런스 또는 nullable 타입에 사용됩니다.
            // if (onSpaceEnter != null) 형태의 코드 대신 사용합니다.
            // 값이 null일 때 리턴값은 null입니다.

        }
    }

    void DebugOnSpaceEnter(object sender, EventArgs e)
    {
        Debug.Log("<color=yellow>엔터 키 입력 이벤트 실행</color>");
    }
}
