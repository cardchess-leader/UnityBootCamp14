using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [Header("발사 설정")]
    [Tooltip("총알 생산 공장")] public GameObject bulletFactory;
    [Tooltip("총구")] public GameObject firePosition;

    private void Update()
    {
        // GetKey, GetKeyDown, GetButton, GetButtonDown 등이 Input 클래스에 정의되어 있다.

        if (Input.GetButtonDown("Fire1"))
        {
            // 총알은 총알 생산 공장에서 등록한 총알을 생성한다.
            // 총알의 위치는 총구 지점으로 설정된다.
            // 별도의 회전은 하지 않는다.
            var bullet = Instantiate(bulletFactory, firePosition.transform.position, Quaternion.identity);

        }
    }
}
