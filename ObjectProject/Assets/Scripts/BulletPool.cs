using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// 오브젝트 풀링(Object Pooling) 기법을 사용하여 총알을 관리하는 클래스
// 자주 생성되고 파괴되는 오브젝트를 미리 일정량 생성해두고 재사용하여 성능을 향상시키는 기법입니다.
// 필요할 때 오브젝트를 가져오고 사용이 끝나면 다시 풀에 반환하는 방식으로 동작합니다.
// 설계 패턴 중 하나로, 게임 개발에서 성능 최적화에 자주 사용됩니다.

// 사용 목적
// 탄알, 이펙트, 데미지 텍스트, 몬스터 처럼 자주 생성되고 사라지는 값들을
// 매번 new, destroy를 통해 생성 삭제가 발생하면 GC가 많이 호출되게 되고
// 이는 성능 저하로 이어질 수 있기에 성능 향상을 목적으로 사용합니다.

// 예시) 발사 총알 수 30개 / 생성될 몬스터 20마리를 미리 한번에 다 생성
//       안쓰는 값은 비활성화

public class BulletPool : MonoBehaviour
{
    public int size = 30;
    public GameObject bulletPrefab;

    // 풀로 자주 사용되는 자료구조
    // 1. 리스트 : 데이터를 순차적으로 저장하고, 추가, 삭제가 자유롭기 때문에 효과적
    // 2. 큐 : 선입선출(FIFO) 구조로, 가장 먼저 들어온 오브젝트가 가장 먼저 나감

    List<GameObject> pool;

    private void Start()
    {
        // 총알 생성및 초기화
        pool = new();

        for (int i = 0; i < size; i++)
        {
            CreateBullet().SetActive(false); // 비활성화 상태로 풀에 저장
        }
    }

    public GameObject GetBullet()
    {

        // 비활성화된 총알을 찾아서 활성화하고 반환
        foreach (GameObject bullet in pool)
        {
            // 계층 창에서 활성화가 안되어있다면 (사용중이 아니라면)
            if (!bullet.activeInHierarchy)
            {
                bullet.SetActive(true); // 활성화
                return bullet;
            }
        }
        // 만약 모든 총알이 활성화 상태라면, 새로 생성 (풀의 크기를 늘림)
        return CreateBullet();
    }

    GameObject CreateBullet()
    {
        GameObject newBullet = Instantiate(bulletPrefab);
        newBullet.transform.SetParent(transform);
        newBullet.GetComponent<Bullet>().SetPool(this); // 총알 풀 설정
        pool.Add(newBullet);
        return newBullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        // 총알을 비활성화하고 풀에 반환
        bullet.SetActive(false);
        bullet.transform.SetParent(transform); // 풀의 자식으로 설정
    }
}
