using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int size = 30;
    public float interval = 2.0f; // Time interval between spawns
    public GameObject enemyPrefab;
    List<GameObject> pool;
    public Transform spawnPoint; // The point where the unit will spawn

    private void Start()
    {
        pool = new();

        for (int i = 0; i < size; i++)
        {
            CreateEnemy().SetActive(false); // ��Ȱ��ȭ ���·� Ǯ�� ����
        }

        StartCoroutine(Spawn());
    }

    public void ReturnEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
    }

    GameObject CreateEnemy()
    {
        GameObject newEnemy = Instantiate(enemyPrefab);
        newEnemy.transform.SetParent(spawnPoint);
        newEnemy.GetComponent<EnemyController>().SetPool(this);
        pool.Add(newEnemy);
        return newEnemy;
    }

    GameObject GetEnemy()
    {
        foreach (GameObject enemy in pool)
        {
            // ���� â���� Ȱ��ȭ�� �ȵǾ��ִٸ� (������� �ƴ϶��)
            if (!enemy.activeInHierarchy)
            {
                enemy.SetActive(true); // Ȱ��ȭ
                return enemy;
            }
        }
        return CreateEnemy();
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            GameObject enemy = GetEnemy();
            enemy.transform.position = spawnPoint.position; // ���� ��ġ ����
            enemy.transform.rotation = spawnPoint.rotation; // ���� ȸ�� ����
            yield return new WaitForSeconds(interval); // ���� ������ ��ٸ��ϴ�.
        }
    }
}
