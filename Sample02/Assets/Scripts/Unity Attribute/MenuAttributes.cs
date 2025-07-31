using UnityEngine;
// Attribute : [AddComponentMenu("")]처럼 클래스나 함수, 변수 앞에 붙는 []들은 Attribute라고 합니다.
// 에디터에 대한 확장이나 사용자 정의 툴 제작에서 제공되는 기능들
// 사용 목적: 사용자가 에디터를 더 직관적으로, 편의적으로 사용하기 위해서

// 1. AddComponentMenu("/그룹이름/기능이름") : 해당 스크립트를 컴포넌트로 추가할 때 메뉴에 표시되도록 합니다.
// 그룹을 지정할 수 있으며, 메뉴 구조를 만들 수 있습니다.
// 순서는 다음과 같은 규칙을 따릅니다:
// - 숫자가 낮을수록 상위 메뉴에 위치합니다.
// - 숫자가 같으면 알파벳 순서로 정렬됩니다.

[AddComponentMenu("Sample/AddComponentMenu", order: 1)]
public class MenuAttributes : MonoBehaviour
{
    // 2. ContextMenu("기능으로 표현할 이름", "함수의 이름") : 해당 함수가 우클릭 메뉴에 표시되도록 합니다.
    [ContextMenuItem("메시지 초기화", "MessageReset")]
    [ContextMenuItem("메시지 초기화2", "MenuAttributesMethod")]
    public string message = "";

    public void MessageReset()
    {
        message = "";
    }

    [ContextMenu("경고 메시지")]
    public void MenuAttributesMethod()
    {
        Debug.LogWarning("경고 메시지!");
    }
}
