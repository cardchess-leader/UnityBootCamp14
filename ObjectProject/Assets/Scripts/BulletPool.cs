using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// ������Ʈ Ǯ��(Object Pooling) ����� ����Ͽ� �Ѿ��� �����ϴ� Ŭ����
// ���� �����ǰ� �ı��Ǵ� ������Ʈ�� �̸� ������ �����صΰ� �����Ͽ� ������ ����Ű�� ����Դϴ�.
// �ʿ��� �� ������Ʈ�� �������� ����� ������ �ٽ� Ǯ�� ��ȯ�ϴ� ������� �����մϴ�.
// ���� ���� �� �ϳ���, ���� ���߿��� ���� ����ȭ�� ���� ���˴ϴ�.

// ��� ����
// ź��, ����Ʈ, ������ �ؽ�Ʈ, ���� ó�� ���� �����ǰ� ������� ������
// �Ź� new, destroy�� ���� ���� ������ �߻��ϸ� GC�� ���� ȣ��ǰ� �ǰ�
// �̴� ���� ���Ϸ� �̾��� �� �ֱ⿡ ���� ����� �������� ����մϴ�.

// ����) �߻� �Ѿ� �� 30�� / ������ ���� 20������ �̸� �ѹ��� �� ����
//       �Ⱦ��� ���� ��Ȱ��ȭ

public class BulletPool : MonoBehaviour
{
    public int size = 30;
    public GameObject bulletPrefab;

    // Ǯ�� ���� ���Ǵ� �ڷᱸ��
    // 1. ����Ʈ : �����͸� ���������� �����ϰ�, �߰�, ������ �����ӱ� ������ ȿ����
    // 2. ť : ���Լ���(FIFO) ������, ���� ���� ���� ������Ʈ�� ���� ���� ����

    List<GameObject> pool;

    private void Start()
    {
        // �Ѿ� ������ �ʱ�ȭ
        pool = new();

        for (int i = 0; i < size; i++)
        {
            CreateBullet().SetActive(false); // ��Ȱ��ȭ ���·� Ǯ�� ����
        }
    }

    public GameObject GetBullet()
    {

        // ��Ȱ��ȭ�� �Ѿ��� ã�Ƽ� Ȱ��ȭ�ϰ� ��ȯ
        foreach (GameObject bullet in pool)
        {
            // ���� â���� Ȱ��ȭ�� �ȵǾ��ִٸ� (������� �ƴ϶��)
            if (!bullet.activeInHierarchy)
            {
                bullet.SetActive(true); // Ȱ��ȭ
                return bullet;
            }
        }
        // ���� ��� �Ѿ��� Ȱ��ȭ ���¶��, ���� ���� (Ǯ�� ũ�⸦ �ø�)
        return CreateBullet();
    }

    GameObject CreateBullet()
    {
        GameObject newBullet = Instantiate(bulletPrefab);
        newBullet.transform.SetParent(transform);
        newBullet.GetComponent<Bullet>().SetPool(this); // �Ѿ� Ǯ ����
        pool.Add(newBullet);
        return newBullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        // �Ѿ��� ��Ȱ��ȭ�ϰ� Ǯ�� ��ȯ
        bullet.SetActive(false);
        bullet.transform.SetParent(transform); // Ǯ�� �ڽ����� ����
    }
}
