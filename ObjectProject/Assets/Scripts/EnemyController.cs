using UnityEngine;
public class EnemyController : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health of the enemy
    private int currentHealth; // Current health of the enemy

    EnemySpawner pool;

    private void OnEnable()
    {
        currentHealth = maxHealth; // Reset current health when the enemy is enabled
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduce current health by damage amount
        if (currentHealth <= 0)
        {
            Die(); // Call Die method if health is zero or below
        }
    }

    public void SetPool(EnemySpawner pool)
    {
        this.pool = pool; // Set the enemy pool reference
    }

    void Die()
    {
        this.pool.ReturnEnemy(gameObject); // Return the enemy to the pool

        var scoreController = FindFirstObjectByType<ScoreController>();
        if (scoreController != null)
        {
            scoreController.IncreaseScore(); // Increase score when enemy dies
        }
    }
}
