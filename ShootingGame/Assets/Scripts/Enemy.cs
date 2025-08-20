using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyType
    {
        Down, // 아래로 내려오는 적 
        Chase // 플레이어를 추적하는 적
    }
    public float speed = 5f; // 적의 이동 속도
    public EnemyType enemyType; // 적의 종류
    public int score = 5; // 적을 처치했을 때 주어지는 점수

    public GameObject explosionPrefab;

    // 적의 패턴
    void Start()
    {
        PatternSetting(); // 패턴 설정 메소드 호출
    }

    private void PatternSetting()
    {
        int rand = Random.Range(0, 10);
        if (rand < 3) // 30% 확률로 Chase 타입으로 설정
        {
            enemyType = EnemyType.Chase; // Chase 타입으로 설정
        }
        else // 나머지 70%는 Down 타입으로 설정
        {
            enemyType = EnemyType.Down; // Down 타입으로 설정
        }
    }

    void Update()
    {
        var dir = Vector3.down;
        if (enemyType == EnemyType.Chase) {
            GameObject target = GameObject.FindGameObjectWithTag("Player");
            dir = (target.transform.position - transform.position).normalized;
        }
        transform.position += dir * speed * Time.deltaTime;
        // also make the enemy to face the direction of movement
        transform.rotation = Quaternion.LookRotation(Vector3.back, -dir);
    }

    void OnCollisionEnter(Collision collision)
    {
        ScoreManager.Instance.AddScore(score); // 점수 추가
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity); // 폭발 이펙트 생성
        collision.gameObject.SetActive(false); // 충돌한 오브젝트 비활성화
        gameObject.SetActive(false); // 자신(enemy) 게임오브젝트 비활성화
        if (collision.gameObject.CompareTag("Player"))
        {
            ScoreManager.Instance.GameOver(); // 플레이어와 충돌 시 게임 오버 처리
        }
    }
}