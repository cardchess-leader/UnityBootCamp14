using UnityEngine;

// ���� ������ ���� Unity MonoBehaviour Ŭ����
public class LinearInter : MonoBehaviour
{
    // Vector3.Lerp(start, end, t) �Լ��� ����Ͽ� ���� ������ �����մϴ�.
    // start -> end���� t�� ���� ���� �����մϴ�.
    // -> �ش� �������� ���� �������� õõ�� �̵��մϴ�.
    // t�� 0���� 1 ������ ������, 0�̸� start ��ġ, 1�̸� end ��ġ�� �ش��մϴ�.
    public Transform target;
    public float speed = 1.0f;

    private Vector3 startPosition;
    private float t = 0;

    private void Start()
    {
        // ���� ��ġ�� ���� ��ġ�� ����
        startPosition = transform.position;
    }

    private void Update()
    {
        if (t < 1.0f)
        {
            // t ���� �������� ������ ����
            t += Time.deltaTime * speed;
            // ������ ��ġ ���
            Vector3 newPosition = Vector3.Lerp(startPosition, target.position, t);
            // ������Ʈ ��ġ ������Ʈ
            transform.position = newPosition;
        }
    }
}
