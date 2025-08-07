using UnityEngine;

// 이 코드는 총알에 대한 발사(생성) 기능만 담당합니다.
public class Fire : MonoBehaviour
{
    // 총알 발사를 위한 풀
    public BulletPool pool;

    // 총알 발사 지점
    public Transform firePoint;

    public GameObject aimRay;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            var bullet = pool.GetBullet();
            bullet.transform.position = firePoint.position; // 총알 위치 설정
            bullet.transform.rotation = firePoint.rotation; // 총알 회전 설정
        }

        // Aim ray visibility with Shift key
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            aimRay.SetActive(true); // Shift 키를 누르면 레이 활성화
        } else
        {
            aimRay.SetActive(false); // Shift 키를 떼면 레이 비활성화
        }

    }
}
