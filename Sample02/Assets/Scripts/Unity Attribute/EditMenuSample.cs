using UnityEngine;

// ������ ��� ���¿��� Update, OnEnable, OnDisable ���� ������ �� �ֵ��� �մϴ�.
// Play�� ���� �ʰ� MonoBehaviour�� �޼��带 ������ �� �ֽ��ϴ�.
[ExecuteInEditMode]
public class EditMenuSample : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ������ ��忡���� ����Ǵ� �ڵ�
        if (!Application.isPlaying)
        {
            Vector3 pos = transform.position;
            pos.y = 0; // y���� 0���� ����
            transform.position = pos;
            Debug.Log("Editor Test...(�� ��ũ��Ʈ�� �� ������Ʈ�� y���� 0���� �����˴ϴ�.");
        }
    }
}
