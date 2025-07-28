using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class VectorTest1 : MonoBehaviour
{
    // ���� ������Ʈ�� Transform�� ���� Vector �� ���ϱ�
    public Transform A_CUBE;
    public Transform B_CUBE;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ���� ť���� ��ġ�� ���ͷ� �����մϴ�.
        Vector3 posA = A_CUBE.position; // A_CUBE�� ��ġ�� �����ɴϴ�.
        Vector3 posB = B_CUBE.position; // B_CUBE�� ��ġ�� �����ɴϴ�.

        Vector3 atob = posB - posA; // A_CUBE���� B_CUBE�� ���ϴ� ���� ���͸� ����մϴ�.
        Vector3 btoa = posA - posB; // B_CUBE���� A_CUBE�� ���ϴ� ���� ���͸� ����մϴ�.

        float distance = Vector3.Distance(posA, posB); // �� ť�� ������ �Ÿ��� ����մϴ�.
        Debug.Log($"Distance from A to B: {distance}"); // �Ÿ� ���
        // �Ÿ� ����
        // Distance�� ������ ����
        // a = (ax, ay, az)
        // b = (bx, by, bz)
        // �� ���� ������ �Ÿ���
        // �� �࿡ ���� ���� ������ �տ� ���� �������� ���մϴ�.
        // distance = ��((bx - ax)�� + (by - ay)�� + (bz - az)��)
        // ����Ƽ�� Mathf Ŭ���� ������� �� ���� �ٲٸ�?
        // distance = Mathf.Sqrt(Mathf.Pow(btoa.x, 2) + Mathf.Pow(btoa.y, 2) + Mathf.Pow(btoa.z, 2));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
