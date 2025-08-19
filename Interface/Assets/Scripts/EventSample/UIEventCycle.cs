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
    // �̺�Ʈ ó���� �Լ�
    // BaseEventData�� �̺�Ʈ �ý��ۿ��� ���Ǵ� �̺�Ʈ �����Ϳ� ���� ���� Ŭ�����Դϴ�.
    private void Handle(string eventName, BaseEventData eventData)
    {
        eventCount++;
        float now = Time.time;
        float delta = now - lastEventTime; // ������ �̺�Ʈ���� �ð� ����
        lastEventTime = now;
        string pos = ""; // ���� ���� PointerEventData�� ��� ��ǥ�� ���� ��� ó��
        // C# pattern matching�� ����Ͽ� eventData�� PointerEventData���� Ȯ��
        // 1. eventData is PointerEventData -> ��ü�� PointerEventData Ÿ������ Ȯ��
        // 2. ������ pointerData ������ eventData�� ĳ����

        if (eventData is PointerEventData pointerData)
        {
            pos = $"pos: {pointerData.position}";
        }

        StringBuilder sb = new StringBuilder();
        sb.Append($"<color=green>EventCount: {eventCount}</color> ");
        sb.Append($"<color=yellow><b>Event: {eventName}</b></color> ");
        sb.Append($"<color=cyan>Delta: {delta:F3} sec</color> ");
        sb.Append($"<color=red>Pos: {pos}</color>");
        // F3�� �Ҽ��� ���� 3�ڸ����� ǥ���ϴ� �����Դϴ�.
        // N2: Number�� ���� ����. 1,234.56 ���·� ǥ��
        // D5: ������ ���� ����. 00001 ���·� ǥ��
        // P1: ������� ���� ����. 12.3% ���·� ǥ�� {0.345:P1}�� 34.5%�� ǥ�õ˴ϴ�.
        Debug.Log(sb.ToString());
    }
    // �̺�Ʈ�� �߻��� ������ Handle �޼��带 ȣ���Ͽ� �̺�Ʈ �̸�, �ð� ����, ��ǥ ���� ����մϴ�.
    // �����ϴ� ��ɹ��� 1���̹Ƿ�, Handle �޼��尡 ȣ��Ǵ� �κп��� ���� �̺�Ʈ �̸��� �����͸� �����մϴ�. (ȭ��ǥ �Լ�)
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
