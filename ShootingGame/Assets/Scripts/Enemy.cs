using UnityEngine;
// 플레이어와 적의 차이
// 플레이어는 컨트롤을 사용자가 진행합니다.
// 적은 AI가 컨트롤을 진행합니다.
public class Enemy : MonoBehaviour
{
    public enum EnemyType
    {
        Down, // 아래로 내려오는 적 
        Chase // 플레이어를 추적하는 적
    }
    public float speed = 5f; // 적의 이동 속도
    public EnemyType enemyType; // 적의 종류
    Vector3 dir;

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
            GameObject target = GameObject.FindGameObjectWithTag("Player"); // 플레이어 오브젝트를 찾습니다.
            dir = (target.transform.position - transform.position).normalized; // 플레이어 방향을 계산합니다.
            enemyType = EnemyType.Chase; // Chase 타입으로 설정
        }
        else // 나머지 70%는 Down 타입으로 설정
        {
            dir = Vector3.down; // 아래로 내려가는 방향으로 설정
            enemyType = EnemyType.Down; // Down 타입으로 설정
        }
    }

    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트의 태그가 "Player"인 경우
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject); // 플레이어 오브젝트 제거
            Destroy(gameObject); // 적 오브젝트 제거
        } else
        {
            Destroy(gameObject);
        }
    }
}