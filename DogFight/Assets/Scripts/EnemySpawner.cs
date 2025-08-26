using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Make this singleton so that it can be accessed from other scripts
    public static EnemySpawner Instance { get; private set; }
    private void Awake()
    {
        // Ensure that there is only one instance of EnemySpawner
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int maxEnemyCount = 100;
    public int currEnemyCount = 0;
    [SerializeField]
    Vector3 minXYZ;
    [SerializeField]
    Vector3 maxXYZ;
    [SerializeField]
    GameObject GameObject;
    [SerializeField]
    float spawnInterval = 2f; // Spawn new enemy every 2 seconds
    float timeSinceLastSpawn = 0f;
    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= spawnInterval)
        {
            if (currEnemyCount >= maxEnemyCount)
                return;
            // Reset the timer
            timeSinceLastSpawn = 0f;
            // Generate random position within the defined bounds
            float x = Random.Range(minXYZ.x, maxXYZ.x);
            float y = Random.Range(minXYZ.y, maxXYZ.y);
            float z = Random.Range(minXYZ.z, maxXYZ.z);
            Vector3 spawnPosition = new Vector3(x, y, z);
            // Instantiate a new enemy at the random position with no rotation
            Instantiate(GameObject, spawnPosition, Quaternion.identity);
        }
    }
}
