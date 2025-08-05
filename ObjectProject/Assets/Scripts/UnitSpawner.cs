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
            Instantiate(unitPrefab, spawnPoint.position, Quaternion.identity); // ������ �����մϴ�. ���� ��ġ�� spawnPoint�� ��ġ�� ȸ���� ����մϴ�.
            Debug.Log(gameObject.name + " has spawned a unit named " + unitPrefab.name);
            yield return new WaitForSeconds(interval); // ���� ������ ��ٸ��ϴ�.
        }
    }
}
