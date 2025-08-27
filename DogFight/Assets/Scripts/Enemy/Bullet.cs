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
            Debug.Log("Bullet hit " + other.name);
            if (other.CompareTag("Player"))
            {
                Debug.Log("Bullet hit player");
                // Damage the player
                var playerHealth = other.GetComponent<Health>();
                if (playerHealth != null)
                {
                    Debug.Log("Bullet damaged player");
                    playerHealth.TakeDamage(5);
                }
            }
            // Destroy the bullet
            Destroy(gameObject);
        }
        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Bullet collided with " + collision.gameObject.name);
            // Destroy the bullet on any collision
            Destroy(gameObject);
        }
    }

}
