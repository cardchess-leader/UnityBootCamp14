using UnityEngine;
// ����Ƽ���� ���Ǵ� Ŭ���� ����� ��ũ��Ʈ�� �ۼ��մϴ�.

// 1. ����Ƽ�� Transform Ŭ���� ���
// Ʈ�������� ����Ƽ �����Ϳ��� ���� ������Ʈ�� ������ �� �ڵ����� �ο��Ǵ� ������Ʈ�Դϴ�.

// Transform�� ���� ������Ʈ�� ��ġ, ȸ��, ũ�⸦ �����մϴ�.
// ������Ʈ�� ����� ȣ���ϴ� GetComponent<T>()�� ������� �ʰ� �ٷ� ����� �����մϴ�.

//Ŭ������ �������ִ� �Ӽ�(Property))�� ����Ͽ� ��ġ, ȸ��, ũ�⸦ ������ �� �ֽ��ϴ�.
// transform.position -> ���� ������Ʈ�� ��ġ�� �����մϴ�.
// transform.rotation -> ���� ������Ʈ�� ȸ���� �����մϴ�.
// Quaternion������ ������ x, y, z, w�� ����մϴ�.
// x, y, z�� ȸ�� ���� ��Ÿ����, w�� ȸ���� ũ�⸦ ��Ÿ���ϴ�.
// transform.forward -> ���� ������Ʈ�� ���� ������ �����մϴ�.
// transform.forward = Vector3.forward; // ���� ������ �����մϴ�.
// transform.forward, up,right�� ���� ���� ������Ʈ�� ����, ����, ������ ������ ��Ÿ���ϴ�.
// transform.eulerAngles -> ���� ������Ʈ�� ȸ���� ���Ϸ� ������ �����մϴ�.
// eulerAngles�� x, y, z ���� �������� ȸ���ϴ� ������ ��Ÿ���ϴ�. (Vector3 ���·� �����մϴ�.)

// �ش� Ŭ������ �������ִ� �ֿ� ����(�޼ҵ�)
// transform.LookAt(Transform target) -> ���� ������Ʈ�� Ÿ���� �ٶ󺸵��� ȸ���մϴ�.
// transform.Rotate(Vector3 eulerAngles) -> ���� ������Ʈ�� ���Ϸ� ������ ȸ����ŵ�ϴ�.
// transform.Translate(Vector3 translation) -> ���� ������Ʈ�� ������ �������� �̵���ŵ�ϴ�.

public class Sample3 : MonoBehaviour
{
    public GameObject go;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(GetComponent<Sample4>().value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
