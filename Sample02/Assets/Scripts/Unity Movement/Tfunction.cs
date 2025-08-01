using UnityEngine;

// �ﰢ �Լ�
// ����Ƽ���� �������ִ� �ﰢ�Լ��� �ַ� ȸ��, ī�޶� ����, �, �����ӿ� ���� ǥ������ ���˴ϴ�.
// Ư¡) ������ �������� ����մϴ�.

public class Tfunction : MonoBehaviour
{
    // ���
    // Sin(Radian) : �־��� ������ Y��ǥ�� ��ȯ�մϴ�.
    // Cos(Radian) : �־��� ������ X��ǥ�� ��ȯ�մϴ�.
    // Tan(Radian) : �־��� ������ Y/X ������ ��ȯ�մϴ�.(����)
    Vector3 pos;
    //float time;

    public void CircleRotate()
    {
        float angleVelocity = 90.0f; // ȸ�� �ӵ� (�� ����)
        float radius = 5.0f; // ���� ������
        float angle = Time.time * angleVelocity; // ȸ�� �ӵ� ����
        float radian = angle * Mathf.Deg2Rad; // ������ �������� ��ȯ

        var x = Mathf.Cos(radian) * radius; // Cosine�� ����Ͽ� X��ǥ ���
        var y = Mathf.Sin(radian) * radius; // Sine�� ����Ͽ� Y��ǥ ���

        transform.position = new Vector3(x, y, 0); // ���� �׸��� �̵�
    }

    public void ButterFly()
    {
        float t = Time.time * 2f;
        float x = Mathf.Sin(t) * 4;
        float y = Mathf.Sin(t * 2f) * 2;

        transform.position = new Vector3(x, y, 0); // ���� ȿ���� �ָ� �̵�
    }

    public void Wave()
    {
        var offset = Mathf.Sin(Time.time * 12.3f) * 3f; // Sin �Լ��� ����Ͽ� Y��ǥ�� �ĵ� ȿ���� ����
        var offset2 = Mathf.Cos(Time.time * 9.9f) * 7f; // Sin �Լ��� ����Ͽ� Y��ǥ�� �ĵ� ȿ���� ����
        transform.position = pos + Vector3.up * offset + Vector3.right * offset2; // ���� ��ġ�� �ĵ� ȿ���� �����Ͽ� �̵�
    }

    void Start()
    {
        pos = transform.position; // ������ �� ������Ʈ�� ���� ��ġ�� ����
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) // ���콺 ���� ��ư Ŭ�� ��
        {
            CircleRotate(); // ���� ȸ�� �Լ� ȣ��
        }
        else if (Input.GetMouseButton(1)) // ���콺 ������ ��ư Ŭ�� ��
        {
            Wave(); // �ĵ� ȿ�� �Լ� ȣ��
        } else if (Input.GetMouseButton(2)) // �ٸ� Ű �Է��� ���� ��
        {
            ButterFly(); // ���� ȿ�� �Լ� ȣ��
        }
    }
}
