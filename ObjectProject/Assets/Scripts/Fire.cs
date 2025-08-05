using UnityEngine;

// �� �ڵ�� �Ѿ˿� ���� �߻�(����) ��ɸ� ����մϴ�.
public class Fire : MonoBehaviour
{
    // �Ѿ� �߻縦 ���� Ǯ
    public BulletPool pool;

    // �Ѿ� �߻� ����
    public Transform firePoint;

    // �Ѿ� �߻� �ӵ�
    public float fireRate = 0.1f;
    public float time = 0;

    public GameObject aimRay;

    private void Update()
    {
        time += Time.deltaTime;
        //if (time < fireRate)
        //    return; // �߻� �ӵ� ����
        if (Input.GetKey(KeyCode.Space)) {
            var bullet = pool.GetBullet();
            bullet.transform.position = firePoint.position; // �Ѿ� ��ġ ����
            bullet.transform.rotation = firePoint.rotation; // �Ѿ� ȸ�� ����
            time = 0; // �ð� �ʱ�ȭ
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
