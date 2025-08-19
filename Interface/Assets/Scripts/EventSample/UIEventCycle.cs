using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIEventCycle : MonoBehaviour,
    // Pointer
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler,
    // Drag
    IInitializePotentialDragHandler,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler,
    // Scroll]]
    IScrollHandler,
    // Selection
    ISelectHandler,
    IDeselectHandler,
    // Navigation / Submit / Cancel
    IMoveHandler,
    ISubmitHandler,
    ICancelHandler,
    // While selected (per-frame)
    IUpdateSelectedHandler
{
    private int eventCount;
    private float lastEventTime;
    // 이벤트 처리용 함수
    // BaseEventData는 이벤트 시스템에서 사용되는 이벤트 데이터에 대한 기초 클래스입니다.
    private void Handle(string eventName, BaseEventData eventData)
    {
        eventCount++;
        float now = Time.time;
        float delta = now - lastEventTime; // 직전의 이벤트와의 시간 차이
        lastEventTime = now;
        string pos = ""; // 받은 값이 PointerEventData일 경우 좌표에 대한 출력 처리
        // C# pattern matching을 사용하여 eventData가 PointerEventData인지 확인
        // 1. eventData is PointerEventData -> 객체가 PointerEventData 타입인지 확인
        // 2. 맞으면 pointerData 변수에 eventData를 캐스팅

        if (eventData is PointerEventData pointerData)
        {
            pos = $"pos: {pointerData.position}";
        }

        StringBuilder sb = new StringBuilder();
        sb.Append($"<color=green>EventCount: {eventCount}</color> ");
        sb.Append($"<color=yellow><b>Event: {eventName}</b></color> ");
        sb.Append($"<color=cyan>Delta: {delta:F3} sec</color> ");
        sb.Append($"<color=red>Pos: {pos}</color>");
        // F3는 소수점 이하 3자리까지 표시하는 포맷입니다.
        // N2: Number에 대한 구분. 1,234.56 형태로 표시
        // D5: 정수에 대한 구분. 00001 형태로 표시
        // P1: 백분율에 대한 구분. 12.3% 형태로 표시 {0.345:P1}는 34.5%로 표시됩니다.
        Debug.Log(sb.ToString());
    }
    // 이벤트가 발생할 때마다 Handle 메서드를 호출하여 이벤트 이름, 시간 차이, 좌표 등을 출력합니다.
    // 실행하는 명령문이 1줄이므로, Handle 메서드가 호출되는 부분에서 직접 이벤트 이름과 데이터를 전달합니다. (화살표 함수)
    // ----- Pointer -----
    public void OnPointerEnter(PointerEventData eventData) => Handle("OnPointerEnter", eventData);
    public void OnPointerExit(PointerEventData eventData) => Handle("OnPointerExit", eventData);
    public void OnPointerDown(PointerEventData eventData) => Handle("OnPointerDown", eventData);
    public void OnPointerUp(PointerEventData eventData) => Handle("OnPointerUp", eventData);
    // ----- Drag -----
    public void OnInitializePotentialDrag(PointerEventData eventData) => Handle("OnInitializePotentialDrag", eventData);
    public void OnBeginDrag(PointerEventData eventData) => Handle("OnBeginDrag", eventData);
    public void OnDrag(PointerEventData eventData) => Handle("OnDrag", eventData);
    public void OnEndDrag(PointerEventData eventData) => Handle("OnEndDrag", eventData);
    // ----- Scroll -----
    public void OnScroll(PointerEventData eventData) => Handle("OnScroll", eventData);
    // ----- Selection -----
    public void OnSelect(BaseEventData eventData) => Handle("OnSelect", eventData);
    public void OnDeselect(BaseEventData eventData) => Handle("OnDeselect", eventData);
    // ----- Navigation / Submit / Cancel -----
    public void OnMove(AxisEventData eventData) => Handle("OnMove", eventData);
    public void OnSubmit(BaseEventData eventData) => Handle("OnSubmit", eventData);
    public void OnCancel(BaseEventData eventData) => Handle("OnCancel", eventData);
    // ----- While selected (every update) -----
    public void OnUpdateSelected(BaseEventData eventData) => Handle("OnUpdateSelected", eventData);
    private void OnEnable()
    {
        eventCount= 0;
        lastEventTime = Time.time;
    }
}
