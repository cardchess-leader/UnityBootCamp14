using UnityEngine;

// �� �ڵ�� �Ѿ˿� ���� �߻�(����) ��ɸ� ����մϴ�.
public class Fire : MonoBehaviour
{
    // �Ѿ� �߻縦 ���� Ǯ
    public BulletPool pool;

    // �Ѿ� �߻� ����
    public Transform firePoint;

    public GameObject aimRay;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            var bullet = pool.GetBullet();
            bullet.transform.position = firePoint.position; // �Ѿ� ��ġ ����
            bullet.transform.rotation = firePoint.rotation; // �Ѿ� ȸ�� ����
        }

        // Aim ray visibility with Shift key
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            aimRay.SetActive(true); // Shift Ű�� ������ ���� Ȱ��ȭ
        } else
        {
            aimRay.SetActive(false); // Shift Ű�� ���� ���� ��Ȱ��ȭ
        }

    }
}
