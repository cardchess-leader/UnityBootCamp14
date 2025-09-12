using System.Collections;
using UnityEngine;

namespace Enemy
{
    // Require a Renderer component to ensure it's always available.
    //[RequireComponent(typeof(Renderer))]
    public class EnemyBehavior : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField]
        private float speed = 5f;
        [SerializeField]
        private float rotationSpeed = 2f;

        [Header("Effects & Rewards")]
        [SerializeField]
        private GameObject explosionEffect;
        [SerializeField]
        private int experiencePoints = 10;

        [Header("Targeting")]
        [SerializeField]
        private GameObject aimTarget;

        //[SerializeField]
        //Renderer skinnedMeshRenderer;

        // --- Cached References ---
        private Transform _playerTransform;
        public Player Target { get; private set; } // Property with a public getter and a private setter
        [SerializeField]
        private Renderer _renderer; // Cache the Renderer component
        private MaterialPropertyBlock _propBlock; // For efficient color changes
        private static readonly int ColorID = Shader.PropertyToID("_Color");

        // Awake is called before Start, ideal for caching components on this GameObject.
        private void Awake()
        {
            //_renderer = GetComponent<Renderer>();
            _propBlock = new MaterialPropertyBlock();
        }

        // Start is used for setup that might rely on other objects.
        private void Start()
        {
            // Find the player ONCE and cache its components.
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                _playerTransform = playerObject.transform;
                Target = playerObject.GetComponent<Player>();
            }
            else
            {
                // Disable this enemy if no player is found to avoid errors.
                Debug.LogError("Player not found! Disabling enemy.", this);
                enabled = false;
                return;
            }

            EnemySpawner.Instance.currEnemyCount++;
        }

        private void Update()
        {
            // If the player reference is lost (e.g., destroyed), do nothing.
            if (_playerTransform == null || Target.isStealthMode)
            {
                return;
            }

            // --- Movement Logic ---
            // Rotate to face the player using the cached transform.
            Vector3 direction = (_playerTransform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

            // Move forward constantly.
            transform.position += transform.forward * Time.deltaTime * speed;
        }

        private void OnDestroy()
        {
            // Use a null check for robustness when the game is closing.
            if (EnemySpawner.Instance != null)
            {
                EnemySpawner.Instance.currEnemyCount--;
            }
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddExp(experiencePoints);
            }

            // It's highly recommended to use an Object Pool for effects like explosions.
            // For now, Instantiate is fine, but pooling is the next optimization step.
            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            Destroy(gameObject);
        }

        public void SetTargeted(bool isTargeted)
        {
            // Use the cached renderer and MaterialPropertyBlock for efficiency.
            _renderer.GetPropertyBlock(_propBlock);
            _propBlock.SetColor(ColorID, isTargeted ? Color.red : Color.white);
            _renderer.SetPropertyBlock(_propBlock);
        }

        public void ShowTarget()
        {
            StartCoroutine(ShowTargetCoroutine());
        }

        private IEnumerator ShowTargetCoroutine()
        {
            if (aimTarget != null)
            {
                aimTarget.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                aimTarget.SetActive(false);
            }
        }
    }
}