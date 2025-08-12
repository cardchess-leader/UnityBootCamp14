using UnityEngine;

// �� �����ͺ� ����

// JSON
// �ܺ� �ؽ�Ʈ ���� ���·� ���� ���� ����
// �����Ϳ� ��Ÿ�� ��� ��� ����
// ������ ������ �����ο� ��
// ex) ���̺� ������, ���� ������, ���� ��ſ� ������ ��(DB ����),
// ���÷� �ٲ� �� �ִ� ������ ������

// ScriptableObject
// Unity�� ������ ���� ��� �� �ϳ�
// �����Ϳ��� ������ ������ ����
// ���������� �ٷ� �ݿ��ǰ�, ��Ÿ�ӿ� ������ �ε��ϰ� ������ ���� (�޸� ȿ�� ����)
// ������ ������ ���� (������, ����Ʈ, ����, ��ų ��)

// PlayerPrefs
// ������ ������ ���忡 ���
// Unity�� �⺻ ���� ������ ���� ���
// Ű-�� ������ ����
// ������Ʈ��, XML, Plist �� ���ο� ����Ǵ� ���
// ����, ����Ʈ �Ϸ� ����, ĳ���� ����, ȯ�� ���� ��


// �����Ϳ��� �ش� ������Ʈ ���� ����
[CreateAssetMenu(fileName = "������", menuName = "Item/������", order = 1)]
public class SOMaker : ScriptableObject
{
    public enum ItemType
    {
        ���, �Һ�, ��Ÿ
    }

    public string itemName;
    public ItemType itemType;
    public string description;
    public int level;
}
