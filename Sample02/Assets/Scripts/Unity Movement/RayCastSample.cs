using UnityEngine;

// Raycast : ���� ��ġ���� Ư�� �������� ������ ���� �浹�ϴ� ������Ʈ�� �����ϴ� ���
// Ư�� ������Ʈ�� �浹 �������� �����ϴ� �۾� ����
// Ư�� ������Ʈ�� ���� �浹 ������ Ȯ���ϴ� �뵵

public class RayCastSample : MonoBehaviour
{
    RaycastHit hit; // RaycastHit ����ü�� ������ �浹�� ������Ʈ�� ���� ������ ��� ����

    // ref : ������ ������ ����
    // out : ������ ������ �����ϰ�, ���� ���� ���� �ʱ�ȭ�� ������ �ʿ䰡 ����
    // Physics.Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance = Mathf.Infinity, int layerMask = Physics.DefaultRaycastLayers)

    // origin ���⿡�� direction �������� maxDistance �Ÿ���ŭ ������ ���, �浹�� ������Ʈ�� ���� ������ hitInfo�� ����
    // hitInfo�� �浹ü�� ���� ������ ��� �ִ� RaycastHit ����ü

    const float length = 20f; // ������ ����
    int layerMask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        // �� ���� �������� ����ĳ��Ʈ �浹 ó��

        // �� �׸���
        Debug.DrawRay(transform.position, transform.forward * length, Color.red); // ������ �ð������� ǥ��

        // ���̾� ����ũ �����ϱ�
        // 1. �浹��Ű�� ���� ���� ���̾ ���� ���� ����
        int ignoreLayer = LayerMask.NameToLayer("Red"); // �浹��Ű�� ���� ���� ���̾�
        layerMask = ~(1 << ignoreLayer);

        // �浹ü ����(����)
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, length, layerMask);
        // RaycastAll : �� �������� �� ���̰� �浹�� �浹ü�� �迭�� return

        // �ݺ������� �ݶ��̴� üũ
        foreach (RaycastHit hit in hits)
        {
            Debug.Log(hit.collider.name + " was hit!");
            hit.collider.gameObject.SetActive(false); // ���� �浹ü�� ���ӿ�����Ʈ ��Ȱ��ȭ
        }
    }

    // Update is called once per frame
    void Update()
    {
        return;
        // ���� ���콺 ��ư�� Ŭ������ ��
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, length, layerMask))
            {
                Debug.Log("������ �߽�!");
                Debug.Log(hit.collider.name);
                hit.collider.gameObject.SetActive(false); // �浹�� ������Ʈ�� ��Ȱ��ȭ

                // ���̾��ũ�� ��Ʈ����ũ�̸�, �� ��Ʈ�� �ϳ��� ���̾ �ǹ��մϴ�.
                // 1 << n�� n��° ���̾ �����ϴ� ����ũ�� �ǹ��մϴ�.
                // ~�� ���� �ۼ��� ~(1 << n)�� n��° ��Ʈ�� ������ ��Ʈ����ũ�� �����մϴ�.
            }
        }

        // ������Ʈ�� ��ġ���� �������� length��ŭ�� ���̿� �ش��ϴ� ����� ������ ��� �ڵ�
        // �ַ� �����ɽ�Ʈ �۾����� �ð������� Ȯ���ϱ� ���� ���
        Debug.DrawRay(transform.position, transform.forward * length, Color.red); // ������ �ð������� ǥ��
    }
}
