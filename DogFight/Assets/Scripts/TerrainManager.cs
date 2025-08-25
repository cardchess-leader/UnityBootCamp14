using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    [SerializeField]
    GameObject buildingPrefab;
    [SerializeField]
    int buildingCount;
    [SerializeField]
    Vector3 startPos;
    [SerializeField]
    Vector3 endPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Create and place buildingPrefab instance between startPos and endPos, buildingCount times (using interpolation)
        for (int i = 0; i < buildingCount; i++)
        {
            float t = (float)i / (buildingCount - 1);
            Vector3 position = Vector3.Lerp(startPos, endPos, t);
            Instantiate(buildingPrefab, position, Quaternion.identity, transform.Find("Buildings"));
        }
    }
}
