using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> terrainPrefabs;
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

        // Randomly choose one terrain and activate it
        if (terrainPrefabs.Count == 0) return;
        var randomIndex = Random.Range(0, terrainPrefabs.Count);
        SetTerrain(randomIndex);
    }

    private void Update()
    {
        // When pressing number key 1, do something
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetTerrain(0);
        }
        // When pressing number key 1, do something
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetTerrain(1);
        }
        // When pressing number key 1, do something
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetTerrain(2);
        }
        // When pressing number key 1, do something
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetTerrain(3);
        }
        // When pressing number key 1, do something
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SetTerrain(4);
        }
        // When pressing number key 1, do something
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SetTerrain(5);
        }
    }

    void SetTerrain(int index)
    {
        if (terrainPrefabs.Count == 0) return;
        foreach (var terrain in terrainPrefabs)
        {
            terrain.SetActive(false);
        }
        terrainPrefabs[index].SetActive(true);
    }
}
