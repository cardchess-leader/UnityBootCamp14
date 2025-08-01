using UnityEngine;
// ī�޶� �������� ���콺 Ŭ�� ��ġ�� ����ĳ��Ʈ ó��

public class CameraRayCastSample : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Ray ray = new Ray(��ġ, ����)
            // ī�޶󿡼� ����� ���̸� ���� ����
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // �浹�� ������Ʈ�� �̸��� ���
                Debug.Log("Hit: " + hit.collider.name);

                // ���� �÷� ����
                Color randomColor = new Color(Random.value, Random.value, Random.value);
                hit.collider.GetComponent<Renderer>().material.color = randomColor;
                // 
                var hitObject = hit.collider.gameObject;
                int change_layer = LayerMask.NameToLayer("Yellow");
                if (change_layer != -1)
                {
                    hitObject.layer = LayerMask.NameToLayer("Yellow");
                }
            }
            else
            {
                Debug.Log("No hit detected.");
            }
        }
    }
}
