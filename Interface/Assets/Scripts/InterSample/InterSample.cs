using UnityEngine;

// �������̽�(inteface)

//���������� interface �̸� {...} �� ������ ���� �ʴ´�.
// ���� ������ class �ڽ�Ŭ������ : �θ�Ŭ������, �������̽�1, �������̽�2 {...}
// Ư¡ : �������� �������̽� ������ �����մϴ�.

// ������ Ŭ���� ��� : ��ɿ� ���� ������ ����, ���� ����� �Ұ����մϴ�.
//                      ���� �����ڿ� ���� ����� �����մϴ�.
// �������̽� ��� : ��ɿ� ���� ���踦 ��Ź��. (�������̽��� ��� ����� �����ؾ� �մϴ�.)
//                   �������̽��� ���� ��� ���� (�������̽��� �������̽��� ��ӹ��� �� �ֽ��ϴ�.)
//                   ���� ��ӿ� ���� ����� �˴ϴ�. (���� �����ϱ⿡ �浹�� �߻����� �ʽ��ϴ�.)
//                   ������ ��ü�� �ᱹ �������̽��� ������ "Ŭ����" ��ü�Դϴ�.
//                   �������� ���ۿ� ���� ���踦 �����ϴ� �����Դϴ�.
//                   �������̽��� ���� �����ڸ� ����� �� �����ϴ�. (public, private, protected ��)

// ���: �������̽��� ���յ��� ���Ƽ�, �������� ���� �ڵ带 ¥�� �����մϴ�.
// ���յ���? ���յ��� ���ٴ� ���� ������ �������� ���ٴ� ���� �ǹ��մϴ�. (����δ� "loose coupling"�̶�� �մϴ�.)
// 

public interface IThrowable
{

}

public interface IWeapon {
}

public interface ICountable {
}

public interface  IPotion
{
}

public interface IUsable
{
    
}

public class Item
{
}

public class Sword : Item, IWeapon {
    
}

public class Javelin : Item, IWeapon, IThrowable, ICountable {

}

public class MaxPotion : Item, IPotion, IUsable
{

}

public class FirePotion : Item, IPotion, IThrowable
{
}

public class InterSample : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
