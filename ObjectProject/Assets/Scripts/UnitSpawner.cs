using System.Collections;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public GameObject unitPrefab; // The prefab to spawn
    public Transform spawnPoint; // The point where the unit will spawn
    public float interval = 2.0f; // Time interval between spawns

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            Instantiate(unitPrefab, spawnPoint.position, Quaternion.identity); // 유닛을 생성합니다. 생성 위치는 spawnPoint의 위치와 회전을 사용합니다.
            Debug.Log(gameObject.name + " has spawned a unit named " + unitPrefab.name);
            yield return new WaitForSeconds(interval); // 생성 간격을 기다립니다.
        }
    }
}
