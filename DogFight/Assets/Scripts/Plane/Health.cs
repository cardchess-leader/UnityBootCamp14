using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        UIController.Instance.UpdateHpText(currentHealth, maxHealth);
    }

    public int GetCurrentHealth() => currentHealth;
    public int GetMaxHealth() => currentHealth;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(50);
        }
    }

    void TakeDamage(int damage)
    {
        // Apply damage, but hp does not go below 0
        currentHealth = Mathf.Max(currentHealth - damage, 0);

        UIController.Instance.UpdateHpText(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            //Die();
        }
    }
}
