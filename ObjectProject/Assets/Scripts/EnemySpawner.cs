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
            CreateEnemy().SetActive(false); // 비활성화 상태로 풀에 저장
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
            // 계층 창에서 활성화가 안되어있다면 (사용중이 아니라면)
            if (!enemy.activeInHierarchy)
            {
                enemy.SetActive(true); // 활성화
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
            enemy.transform.position = spawnPoint.position; // 스폰 위치 설정
            enemy.transform.rotation = spawnPoint.rotation; // 스폰 회전 설정
            yield return new WaitForSeconds(interval); // 생성 간격을 기다립니다.
        }
    }
}
