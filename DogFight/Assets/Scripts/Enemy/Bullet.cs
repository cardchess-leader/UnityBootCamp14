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
                // GetComponentInParent를 사용하여 계층구조를 따라 올라가며 Health 컴포넌트를 찾음
                var playerHealth = other.GetComponentInParent<Health>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(5);
                }
                else
                {
                    // Health 컴포넌트가 싱글톤으로 구현되어 있으므로 대안으로 Instance 사용
                    Health.Instance?.TakeDamage(5);
                }
            }
            // Destroy the bullet
            Destroy(gameObject);
        }
    }
}
