using UnityEngine;
using System.Collections.Generic;

// Bullet Object Pool을 사용하는 이유는 다음과 같습니다:
// 1. 성능 최적화: 매번 총알을 생성하는 대신, 미리 생성된 총알을 재사용하여 성능을 향상시킵니다.
// 2. 메모리 관리: 오브젝트 풀링을 통해 메모리 해제를 최소화하여 GC의 호출 빈도를 줄입니다.

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
        // 오브젝트 풀에서 비활성화 상태인 총알을 찾아 반환합니다.
        foreach (var bullet in bullets)
        {
            if (!bullet.gameObject.activeInHierarchy)
            {
                bullet.gameObject.SetActive(true);
                return bullet;
            }
        }
        // 비활성화 상태인 총알이 없다면 새로운 총알을 생성합니다.
        var newBullet = Instantiate(bulletPrefab);
        bullets.Add(newBullet);
        return newBullet;
    }
}
