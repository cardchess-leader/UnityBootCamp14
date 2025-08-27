using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Move forward at a accelerating speed
    [SerializeField]
    float bulletSpeed;
    // Include a explosion effect on impact in the future
    [SerializeField]
    GameObject explosionEffect;
    
    private float lifetime = 5f;
    private float currentLifetime;
    private PooledBullet pooledBullet;

    private void Awake()
    {
        // Cache the PooledBullet component reference
        pooledBullet = GetComponent<PooledBullet>();
    }

    private void OnEnable()
    {
        // Reset lifetime when bullet is reused from pool
        currentLifetime = 0f;
    }

    void Update()
    {
        // Move forward
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
        
        // Track lifetime
        currentLifetime += Time.deltaTime;
        
        // Return to pool after lifetime expires
        if (currentLifetime >= lifetime)
        {
            ReturnToPool();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Instantiate explosion effect here
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
        
        // Return to pool instead of destroying
        ReturnToPool();
    }

    public void ResetBullet()
    {
        currentLifetime = 0f;
    }

    private void ReturnToPool()
    {
        if (pooledBullet != null)
        {
            pooledBullet.ReturnToPool();
        }
        else
        {
            // Fallback if not pooled
            Destroy(gameObject);
        }
    }
}
