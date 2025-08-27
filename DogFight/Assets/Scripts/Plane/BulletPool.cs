using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    int poolSize = 20;
    [SerializeField]
    int maxPoolSize = 100;

    // Unity's built-in ObjectPool
    private ObjectPool<GameObject> bulletPool;

    // Make this singleton for easy access
    public static BulletPool Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Initialize Unity's ObjectPool
        bulletPool = new ObjectPool<GameObject>(
            createFunc: CreateBullet,
            actionOnGet: OnGetBullet,
            actionOnRelease: OnReleaseBullet,
            actionOnDestroy: OnDestroyBullet,
            collectionCheck: true,
            defaultCapacity: poolSize,
            maxSize: maxPoolSize
        );
    }

    void Start()
    {
        // Pre-warm the pool
        var bullets = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            bullets.Add(bulletPool.Get());
        }
        
        // Return all bullets to pool
        foreach (var bullet in bullets)
        {
            bulletPool.Release(bullet);
        }
    }

    public GameObject GetBullet()
    {
        return bulletPool.Get();
    }

    public void ReturnBullet(GameObject bullet)
    {
        bulletPool.Release(bullet);
    }   

    // Pool callbacks
    private GameObject CreateBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform);
        
        // Add a component to handle automatic return to pool
        var pooledBullet = bullet.GetComponent<PooledBullet>();
        if (pooledBullet == null)
        {
            pooledBullet = bullet.AddComponent<PooledBullet>();
        }
        pooledBullet.Pool = this;
        
        return bullet;
    }

    private void OnGetBullet(GameObject bullet)
    {
        bullet.SetActive(true);
    }

    private void OnReleaseBullet(GameObject bullet)
    {
        bullet.SetActive(false);
    }

    private void OnDestroyBullet(GameObject bullet)
    {
        Destroy(bullet);
    }

    // Pool statistics (for debugging)
    public int CountInactive => bulletPool.CountInactive;
    public int CountActive => bulletPool.CountActive;
    public int CountAll => bulletPool.CountAll;
}

// Helper component for automatic pool return
public class PooledBullet : MonoBehaviour
{
    public BulletPool Pool { get; set; }
    private bool isBeingReturned = false;
    
    public void ReturnToPool()
    {
        if (Pool != null && !isBeingReturned)
        {
            isBeingReturned = true;
            Pool.ReturnBullet(gameObject);
        }
    }
    
    private void OnDisable()
    {
        // Reset the flag when disabled
        isBeingReturned = false;
    }
}
