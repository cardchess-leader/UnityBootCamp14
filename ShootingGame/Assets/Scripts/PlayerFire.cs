using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [Header("�߻� ����")]
    [Tooltip("�Ѿ� ���� ����")] public GameObject bulletFactory;
    [Tooltip("�ѱ�")] public GameObject firePosition;

    private void Update()
    {
        // GetKey, GetKeyDown, GetButton, GetButtonDown ���� Input Ŭ������ ���ǵǾ� �ִ�.

        if (Input.GetButtonDown("Fire1"))
        {
            // �Ѿ��� �Ѿ� ���� ���忡�� ����� �Ѿ��� �����Ѵ�.
            // �Ѿ��� ��ġ�� �ѱ� �������� �����ȴ�.
            // ������ ȸ���� ���� �ʴ´�.
            var bullet = Instantiate(bulletFactory, firePosition.transform.position, Quaternion.identity);

        }
    }
}
