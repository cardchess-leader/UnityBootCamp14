using UnityEngine;

public class Enemy : MonoBehaviour
{
    //public static int maxEnemyCount = 20;
    //public static int enemyCount = 0;
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
        EnemySpawner.Instance.currEnemyCount++;
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

    private void OnDestroy()
    {
        EnemySpawner.Instance.currEnemyCount--;
    }

    // When collided with another object, destroy itself
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        GameManager.Instance.AddExp(10);
    }

    public void SetTargeted(bool isTargeted)
    {
        // Change color to indicate targeting status
        Renderer renderer = GetComponent<Renderer>();
        if (isTargeted)
            renderer.material.color = Color.red;
        else
            renderer.material.color = Color.white;
    }
}

