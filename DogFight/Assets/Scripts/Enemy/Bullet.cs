using UnityEngine;
namespace Enemy
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        float speed = 10f;

        // Move towards the player
        void Update()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            //Debug.Log($"Bullet collided with: {other.name}");
            if (other.CompareTag("Player"))
            {
                // GetComponentInParent�� ����Ͽ� ���������� ���� �ö󰡸� Health ������Ʈ�� ã��
                var playerHealth = other.GetComponentInParent<Health>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(5);
                }
                else
                {
                    // Health ������Ʈ�� �̱������� �����Ǿ� �����Ƿ� ������� Instance ���
                    Health.Instance?.TakeDamage(5);
                }
            }
            // Destroy the bullet
            Destroy(gameObject);
        }
    }
}
