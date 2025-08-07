using System.Collections.Generic;
using System.Collections;
using UnityEngine;

// 총알에 대한 정보, 총알 반납, 총알 이동
public class Bullet : MonoBehaviour
{
    public float speed = 20f; // 총알 속도
    public float lifeTime = 2f; // 총알 생명 시간
    public int damage; // 총알이 입히는 데미지
    public float criChance = 0.1f; // 크리티컬 확률
    public List<GameObject> effectPrefabs;

    BulletPool pool; // 총알 풀 참조
    Coroutine lifeCoroutine;

    private void OnEnable()
    {
        lifeCoroutine = StartCoroutine(LifeCoroutine());
    }

    private void OnDisable()
    {
        if (lifeCoroutine != null)
        {
            StopCoroutine(lifeCoroutine);// 코루틴 중지
            lifeCoroutine = null; // 참조 해제
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime); // 총알 이동
    }

    IEnumerator LifeCoroutine()
    {
        yield return new WaitForSeconds(lifeTime); // 생명 시간 대기
        ReturnPool(); // 생명 시간이 끝나면 총알을 풀에 반환
    }

    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트가 Enemy 태그를 가진 경우
        if (other.CompareTag("Enemy"))
        {
            bool isCritical = false;
            // 10% 확률로 크리티컬 히트 발생
            if (Random.Range(0f, 1f) < criChance)
            {
                isCritical = true;
            }
            other.GetComponent<EnemyController>()?.TakeDamage(isCritical ? int.MaxValue : damage); // EnemyHealth 컴포넌트에서 데미지 처리

            // effectPrefabs는 크기가 2개
            // 0번 인덱스는 일반 이펙트, 1번 인덱스는 크리티컬 이펙트
            int idx = isCritical ? 1 : 0; // 크리티컬 여부에 따라 이펙트 인덱스 선택
            // 해당 인덱스의 이펙트 프리팹을 인스턴스화
            if (effectPrefabs == null || effectPrefabs.Count <= idx)
            {
                Debug.LogWarning("Effect prefabs not set or index out of range.");
                return;
            }
            Instantiate(effectPrefabs[idx], transform.position, Quaternion.identity);
        }

        ReturnPool(); // 충돌 시 총알을 풀에 반환
    }

    public void SetPool(BulletPool bulletPool)
    {
        this.pool = bulletPool; // 총알 풀 설정
    }

    void ReturnPool() => pool.ReturnBullet(gameObject);
}
