using System;
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
                //Debug.Log($"other name is: {other.name}");
                // Damage the player
                var playerHealth = other.transform.parent.GetComponent<Health>();
                if (playerHealth != null)
                {
                    //Debug.Log("Bullet damaged player");
                    playerHealth.TakeDamage(5);
                }
            }
            // Destroy the bullet
            Destroy(gameObject);
        }
    }
}
