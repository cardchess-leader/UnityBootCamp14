using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab;
    public float spawnDelay = 1;
    float timeSinceLastSpawn = 0;

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnDelay)
        {
            // Spawn a new object
            GameObject go = Instantiate(objectPrefab);
            float randomX = Random.Range(-10f, 10f);
            go.transform.position = new Vector3(randomX, transform.position.y, transform.position.z);
            timeSinceLastSpawn -= spawnDelay;
        }
    }
}
