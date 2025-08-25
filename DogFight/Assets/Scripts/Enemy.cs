using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    GameObject explosionEffect;
    GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Find the player object by tag
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate to face the player
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2f);
        // Move forward constantly
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    // When collided with another object, destroy itself
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
    }
}

