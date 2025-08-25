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

    private void OnEnable()
    {
        // Reset lifetime when bullet is reused from pool
        currentLifetime = 0f;
    }
    void Update()
    {
        // Accelerate forward
        transform.position += transform.up * bulletSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Destroy the missile on impact
        Destroy(gameObject);
        // Optionally, instantiate explosion effect here
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
    }
}
