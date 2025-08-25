using Unity.VisualScripting;
using UnityEngine;

public class Missile : MonoBehaviour
{
    // Move forward at a accelerating speed
    [SerializeField]
    float acceleration;
    [SerializeField]
    float maxSpeed;
    float initialSpeed;
    float currentSpeed;
    // Include a explosion effect on impact in the future
    [SerializeField]
    GameObject explosionEffect;

    private void Start()
    {
        // Destroy after 10 seconds to avoid clutter
        Destroy(gameObject, 10f);
        currentSpeed = initialSpeed;
    }
    void Update()
    {
        // Accelerate forward
        transform.position += transform.up * currentSpeed * Time.deltaTime;
        currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.deltaTime, maxSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Destroy the missile on impact
        Destroy(gameObject);
        // Optionally, instantiate explosion effect here
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
    }

    public void SetInitSpeed(float initialSpeed)
    {
        this.initialSpeed = Mathf.Min(initialSpeed + 10, maxSpeed); // Add a small boost to the initial speed
    }
}
