using UnityEngine;

// ���콺 �浹 ���� ��ȭ ���� ���� (ī�޶� ����)
public class DMRaycaster : MonoBehaviour
{
    public float rayLength = 10f; // ������ ����
    public LayerMask layerMask; // �浹�� ������ ���̾� ����ũ
    Camera mainCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main; // ���� ī�޶� �����ɴϴ�.
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư Ŭ�� ��
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); // ���콺 ��ġ���� ���̸� ����

            if (Physics.Raycast(ray, out RaycastHit hit, rayLength, layerMask)) // ���̰� �浹�ϴ��� Ȯ��
            {
                // Ʈ���� üũ
                DTrigger dTrigger = hit.collider.GetComponent<DTrigger>(); // �浹�� ������Ʈ���� DTrigger ������Ʈ�� ã��
                if (dTrigger != null) // DTrigger�� �����ϸ�
                {
                    dTrigger.OnDTriggerEnter(); // ��ȭ�� �����մϴ�.
                }
            }
        }
    }
}
