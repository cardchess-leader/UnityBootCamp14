using System.Collections.Generic;
using System.Collections;
using UnityEngine;

// 총알에 대한 정보, 총알 반납, 총알 이동
public class Bullet : MonoBehaviour
{
    public float speed = 20f; // 총알 속도
    public float lifeTime = 2f; // 총알 생명 시간
    public int damage = 1; // 총알이 입히는 데미지
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
            other.GetComponent<EnemyController>()?.TakeDamage(damage); // EnemyHealth 컴포넌트에서 데미지 처리
        }
        // 부딪친 오브젝트가 Enemy 태그를 가진 경우
        // 데미지를 입힙니다.
        // 이펙트 연출(파티클)
        if (effectPrefabs != null && effectPrefabs.Count > 0)
        {
            int idx = Random.Range(0, effectPrefabs.Count);
            var effectPrefab = effectPrefabs[idx];
            if (effectPrefab != null)
                Instantiate(effectPrefab, transform.position, Quaternion.identity);
        }

        ReturnPool(); // 충돌 시 총알을 풀에 반환
    }

    public void SetPool(BulletPool bulletPool)
    {
        this.pool = bulletPool; // 총알 풀 설정
    }

    void ReturnPool() => pool.ReturnBullet(gameObject);
}
