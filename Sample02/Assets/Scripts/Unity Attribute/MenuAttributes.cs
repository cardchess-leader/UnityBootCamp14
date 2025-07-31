using UnityEngine;
// Attribute : [AddComponentMenu("")]ó�� Ŭ������ �Լ�, ���� �տ� �ٴ� []���� Attribute��� �մϴ�.
// �����Ϳ� ���� Ȯ���̳� ����� ���� �� ���ۿ��� �����Ǵ� ��ɵ�
// ��� ����: ����ڰ� �����͸� �� ����������, ���������� ����ϱ� ���ؼ�

// 1. AddComponentMenu("/�׷��̸�/����̸�") : �ش� ��ũ��Ʈ�� ������Ʈ�� �߰��� �� �޴��� ǥ�õǵ��� �մϴ�.
// �׷��� ������ �� ������, �޴� ������ ���� �� �ֽ��ϴ�.
// ������ ������ ���� ��Ģ�� �����ϴ�:
// - ���ڰ� �������� ���� �޴��� ��ġ�մϴ�.
// - ���ڰ� ������ ���ĺ� ������ ���ĵ˴ϴ�.

[AddComponentMenu("Sample/AddComponentMenu", order: 1)]
public class MenuAttributes : MonoBehaviour
{
    // 2. ContextMenu("������� ǥ���� �̸�", "�Լ��� �̸�") : �ش� �Լ��� ��Ŭ�� �޴��� ǥ�õǵ��� �մϴ�.
    [ContextMenuItem("�޽��� �ʱ�ȭ", "MessageReset")]
    [ContextMenuItem("�޽��� �ʱ�ȭ2", "MenuAttributesMethod")]
    public string message = "";

    public void MessageReset()
    {
        message = "";
    }

    [ContextMenu("��� �޽���")]
    public void MenuAttributesMethod()
    {
        Debug.LogWarning("��� �޽���!");
    }
}
