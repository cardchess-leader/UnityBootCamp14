using UnityEngine;

// �Ϲ����� Lerp�� Slerp�� ���Ǵ� ���

//1. �ܼ��� ��ġ �̵� -> Lerp
//2. ȸ�� �� ���� ��ȯ -> Slerp
//3. �ڿ������� ī�޶��� ������ -> Slerp
// ���: Lerp�� ���� ������ ����Ͽ� �� �� ���̸� �������� �̵���Ű��, Slerp�� ���� ������ ����Ͽ� �� �� ���̸� ����� �̵���ŵ�ϴ�.
// Lerp: ü�� ������ ���� ���� ������ �ʿ��� ��쿡 ����մϴ�.
// Slerp: ȸ���̳� ���� ��ȯ�� �ʿ��� ��쿡 ����մϴ�.3D ȸ��(Quaternion), ���� ���� � ��� Ȯ��, ���� ��ȯ�� �ε巴�� ����� �ٶ󺸵��� �� �� ����մϴ�.
public class SphericalInter : MonoBehaviour
{
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
            Vector3 newPosition = Vector3.Slerp(startPosition, target.position, t);
            // ������Ʈ ��ġ ������Ʈ
            transform.position = newPosition;
        }
    }
}
