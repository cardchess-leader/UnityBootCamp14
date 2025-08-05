using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rotSpeed = 0.1f; // Rotation speed
    Vector3 lastMousePos;
    int maxHealth = 100;
    int currentHealth;

    float pitch = 0f; // Camera pitch (X rotation)

    void OnEnable()
    {
        currentHealth = maxHealth; // Initialize current health to maximum health
        lastMousePos = Input.mousePosition; // Store initial mouse position
    }

    private void Update()
    {
        float moveSpeed = 5f;
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(-moveVertical, 0.0f, moveHorizontal);
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.Self);

        var currMousePos = Input.mousePosition;
        transform.Rotate(0, (currMousePos.x - lastMousePos.x) * rotSpeed, 0);
        pitch -= (currMousePos.y - lastMousePos.y) * rotSpeed;
        pitch = Mathf.Clamp(pitch, -45f, 45f); // Clamp pitch to prevent flipping
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, pitch);

        lastMousePos = Input.mousePosition; // Update last mouse position
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(100); // Example damage value
            }
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
        var scoreController = FindFirstObjectByType<ScoreController>();
        if (scoreController != null)
        {
            scoreController.ShowGameOver();
        }
    }
}
