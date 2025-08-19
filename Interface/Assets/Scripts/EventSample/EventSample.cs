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
        }
    }
}
