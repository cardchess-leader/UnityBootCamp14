using UnityEngine;

// �÷��̾ 45�� �������� �����ϴ� �ڵ�

public class Anglemove : MonoBehaviour
{
    [SerializeField] 
    float angle_degree; // �̵��� ���� (�� ����)
    [SerializeField]
    float speed; // �̵� �ӵ�

    // Update is called once per frame
    void Update()
    {
        var radian = angle_degree * Mathf.Deg2Rad; // ������ �������� ��ȯ
        Vector3 dir = new Vector3(Mathf.Cos(radian), 0, Mathf.Sin(radian)); // �̵� ���� ���

        transform.Translate(dir * speed * Time.deltaTime, Space.World); // ���� ��ǥ�迡�� �̵�

        if (Input.GetKeyDown(KeyCode.Return)) // Enter Ű�� ������ ��
        {
            transform.position = Vector3.zero; // ������Ʈ�� �������� �̵�
        }
    }
}
