using UnityEngine;

// ���ʹϾ� ��� ����
// Quaternion.identity : ���� ���ʹϾ�, ȸ���� ���� ����
// Quaternion.Euler(x, y, z) : ���Ϸ� ���� ����Ͽ� ���ʹϾ� ����
// Quaternion.AngleAxis(angle, axis) : �־��� ������ ���� ����Ͽ� ���ʹϾ� ����
// Quaternion.Lerp(a, b, t) : a���� b�� ���� �����ϴ� ���ʹϾ� ����
// Quaternion.Slerp(a, b, t) : a���� b�� ���� ���� �����ϴ� ���ʹϾ� ����
// Quaternion.Inverse(q) : �־��� ���ʹϾ��� ���� ���
// Quaternion.LookRotation(forward, up) : �־��� ����(forward)�� ����(up)�� ����Ͽ� ���ʹϾ� ����
// Quaternion.Dot(a, b) : �� ���ʹϾ��� ���� ���
// Quaternion.Normalize(q) : �־��� ���ʹϾ��� ����ȭ
// Quaternion.Angle(a, b) : �� ���ʹϾ� ������ ���� ���
// Quaternion.ToEulerAngles() : ���ʹϾ��� ���Ϸ� ������ ��ȯ
// forward: ȸ����ų ����
// up: ȸ����ų ���� ����

// ȸ�� �� ����
// transform.rotation = Quaternion.Euler(0, 90, 0); // ���� ������Ʈ�� ȸ�� ���� �����մϴ�.

// ȸ���� ���� ����
// Quaternion.Lerp(a, b, t) : a���� b�� ���� �����ϴ� ���ʹϾ� ����
// Quaternion.Slerp(a, b, t) : a���� b�� ���� ���� �����ϴ� ���ʹϾ� ����
// Quaternion.RotateTowards(from, to, maxDegreesDelta) : from���� to�� ȸ���ϴ� ���ʹϾ� ����

public class QuaternionSample : MonoBehaviour
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

// transform.LookAt(target); // Ÿ���� �ٶ󺸴� ȸ�� ����

// transform.LookRotation(Vector3.forward, Vector3.up); // Ư�� ������ �ٶ󺸴� ȸ�� ����