using UnityEngine;

// ��ǥ �������� ȸ���ϴ� MonoBehaviour ��ũ��Ʈ
public class TargetRotate : MonoBehaviour
{
    public Transform target; // ȸ���� ����� Transform
    public float rotationSpeed = 90f; // ȸ�� �ӵ�
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Quaternion.LookRotation(Vector3 forward); Ư�� ������ �ٶ󺸴� ȸ���� ���
        var targetRotation = Quaternion.LookRotation(target.position - transform.position);
        // ������ ȸ������ Ÿ���� ȸ������ ���� �ӵ��� �ε巴�� ȸ���� �����ϴ� �Լ�
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
