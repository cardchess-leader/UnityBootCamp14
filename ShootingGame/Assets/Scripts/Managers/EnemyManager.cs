using UnityEngine;

// 목표: 일정 시간마다 적을 생성해 내 위치에 놓을 것.
// 필요한 데이터: 일정 시간, 현재 시간, 적 생성 공장
// 작업 순서: 1. 시간을 체크하고 2. 시간이 되면 적을 생성하고(쿨타임) 3. 적을 내 위치에 놓기

public class EnemyManager : MonoBehaviour
{
    float min = 1, max = 5; // 소환 시간 간격(최대 최소)
    float timeSinceLastSpawn = 0;
    public float coolTime;
    public GameObject enemyPrefab;
    public GameObject spawnArea;


    private void Start()
    {
        coolTime = Random.Range(min, max) * coolTime;
        ScoreManager.Instance.OnStageClear.AddListener(DisableEnemyManager);
    }

    void DisableEnemyManager() // 스테이지 클리어 시 적 생성 중지
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= coolTime)
        {
            var enemy = Instantiate(enemyPrefab, spawnArea.transform.position, enemyPrefab.transform.rotation);
            timeSinceLastSpawn = 0; // 쿨타임 초기화
            coolTime = Random.Range(min, max); // 다음 소환 시간 간격을 랜덤으로 설정
        }
    }
}
