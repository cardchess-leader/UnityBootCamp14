using UnityEngine;
using UnityEngine.EventSystems;

// 1. 이벤트 시스템 컴포넌트가 씬에 존재해야 합니다.
// 2. UI의 경우 피직스 레이캐스터 컴포넌트가 추가되어있어야 하며, Raycast의 Target도 체크가 되어있어야 합니다.
// 3. 오브젝트의 경우에는 Collider 컴포넌트가 추가되어있어야 합니다.
// 4. 메인 카메라에 Physics Raycaster 컴포넌트가 추가되어 있어야 합니다.

// 제공되는 기능 기준 일반적인 순서
// 1. Pointer Enter
// 2. Pointer Down
// 3. Begin Drag
// 4. Drag
// 5. End Drag
// 6. Pointer Up
// 7. Pointer Exit
// 8. Select
// 9. Submit
// 10. Move
// 11. Cancel

public class ClickTester : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{



    // PointerEventData : 마우스 클릭 시의 클릭 버튼, 클릭 횟수, 포인터 위치 등에 대한 정보를 담고 있는 클래스입니다.
    // 마우스로 1회 클릭하는 경우
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("터치나 마우스 포인터의 위치: " + eventData.position);
        Debug.Log("클릭 횟수: " + eventData.clickCount);
        Debug.Log("클릭 시간 : " + eventData.clickTime);
        Debug.Log("포인터의 이동량: " + eventData.delta);
        Debug.Log("현재 드래그 중인지 여부: " + eventData.dragging);
        Debug.Log("클릭한 버튼: " + eventData.button);
        Debug.Log("포인터의 ID: " + eventData.pointerId);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("포인터 눌렀을 때");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("포인터 탈출~");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("포인터 뗐을 때");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
