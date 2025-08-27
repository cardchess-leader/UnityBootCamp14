using UnityEngine;

public class Health : MonoBehaviour
{
    // Make this a singleton so that it can be accessed from other scripts
    public static Health Instance { get; private set; }
    private void Awake()
        {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


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

    public void TakeDamage(int damage)
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
