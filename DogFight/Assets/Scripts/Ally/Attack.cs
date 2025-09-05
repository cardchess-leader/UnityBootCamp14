using UnityEngine;
using Enemy;

namespace Ally
{
    public class Attack : MonoBehaviour
    {
        [SerializeField]
        GameObject bullet;
        EnemyBehavior targetEnemy;
        [SerializeField] float attackCooldown = 1f;
        float timeSinceLastAttack = 0f;
        [SerializeField] float detectionRange = 100;


        // Update is called once per frame
        void Update()
        {
            // Find the nearest enemy (Within a certain range)
            if (targetEnemy == null)
            {
                FindNearestEnemy();
            }
            // Head towards the enemy slowly
            if (targetEnemy != null)
            {
                Vector3 direction = (targetEnemy.transform.position - transform.position).normalized;
                transform.position += direction * Time.deltaTime * 2f; // Move towards the enemy at a speed of 2 units per second
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // Smoothly rotate towards the enemy
            }
            // Attack the enemy if in range and cooldown is over
            timeSinceLastAttack += Time.deltaTime;
            if (timeSinceLastAttack >= attackCooldown)
            {
                timeSinceLastAttack = 0f;
                AttackEnemy();
            }
        }

        void FindNearestEnemy()
        {
            EnemyBehavior[] enemies = FindObjectsByType<EnemyBehavior>(FindObjectsSortMode.None);
            float nearestDistance = Mathf.Infinity;
            foreach (EnemyBehavior enemy in enemies)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < nearestDistance && distance < detectionRange) // Assuming 10f is the attack range
                {
                    nearestDistance = distance;
                    targetEnemy = enemy;
                }
            }
        }

        void AttackEnemy()
        {
            if (targetEnemy == null) return;
            // Check if this gameobject is looking at the enemy (within a small angle)
            Vector3 directionToEnemy = (targetEnemy.transform.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, directionToEnemy);
            if (angle > 10f) return; // Not looking at the enemy
            // Shoot at the enemy
            Vector3 direction = (targetEnemy.transform.position - transform.position).normalized;
            GameObject newBullet = Instantiate(bullet, transform.position + direction * 2f, Quaternion.LookRotation(direction));
            Destroy(newBullet, 5f); // Destroy bullet after 5 seconds to avoid clutter
            // Reset target after attack to find a new one in the next frame
            targetEnemy = null;
        }
    }
}