using UnityEngine;
using System.Collections.Generic;

// Bullet Object Pool�� ����ϴ� ������ ������ �����ϴ�:
// 1. ���� ����ȭ: �Ź� �Ѿ��� �����ϴ� ���, �̸� ������ �Ѿ��� �����Ͽ� ������ ����ŵ�ϴ�.
// 2. �޸� ����: ������Ʈ Ǯ���� ���� �޸� ������ �ּ�ȭ�Ͽ� GC�� ȣ�� �󵵸� ���Դϴ�.

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance = null;
    List<GameObject> bullets = new();
    public GameObject bulletPrefab;
    public int initialPoolSize = 30;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        // Initialize the bullet pool with a specified size
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.gameObject.SetActive(false);
            bullets.Add(bullet);
        }
    }

    public GameObject GetBullet()
    {
        // ������Ʈ Ǯ���� ��Ȱ��ȭ ������ �Ѿ��� ã�� ��ȯ�մϴ�.
        foreach (var bullet in bullets)
        {
            if (!bullet.gameObject.activeInHierarchy)
            {
                bullet.gameObject.SetActive(true);
                return bullet;
            }
        }
        // ��Ȱ��ȭ ������ �Ѿ��� ���ٸ� ���ο� �Ѿ��� �����մϴ�.
        var newBullet = Instantiate(bulletPrefab);
        bullets.Add(newBullet);
        return newBullet;
    }
}
