using UnityEngine;

// ĳ��(Cache)?
// ���� ���Ǵ� �����ͳ� ���� �̸� �����صδ� �ӽ� ���

public class Sample3 : MonoBehaviour
{
    Rigidbody rb;
    Vector3 velocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // ĳ��(caching): GetComponent�� Start���� �� ���� ȣ���Ͽ� ������ ����ŵ�ϴ�.
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().AddForce(velocity * 5); // �� �����Ӹ��� Rigidbody�� ���� �߰��մϴ�.
    }
}
