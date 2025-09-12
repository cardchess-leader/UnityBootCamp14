using UnityEngine;

namespace Enemy
{
    public class Attack : MonoBehaviour
    {
        [SerializeField]
        GameObject bullet;
        [SerializeField]
        float range;
        [SerializeField]
        float shootCooldown = 1f;
        float timeSinceLastShot = 0f;
        private Player target => GetComponent<EnemyBehavior>().Target;

        // When player is in range, shoot at them
        void Update()
        {
            // Simple shooting logic for demonstration
            if (!target.isStealthMode && Vector3.Distance(transform.position, target.transform.position) < range)
            {
                timeSinceLastShot += Time.deltaTime;
                if (timeSinceLastShot >= shootCooldown)
                {
                    timeSinceLastShot = 0f;
                    Shoot();
                }
            }
        }

        void Shoot()
        {
            // Instantiate a bullet and set its direction towards the player
            Vector3 direction = (target.transform.position - transform.position).normalized;
            GameObject newBullet = Instantiate(bullet, transform.position + direction * 2f, Quaternion.LookRotation(direction));
            Destroy(newBullet, 5f); // Destroy bullet after 5 seconds to avoid clutter
        }
    }
}
