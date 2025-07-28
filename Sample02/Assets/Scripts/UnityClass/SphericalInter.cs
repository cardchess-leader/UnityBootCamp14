using UnityEngine;

// 일반적인 Lerp와 Slerp가 사용되는 경우

//1. 단순한 위치 이동 -> Lerp
//2. 회전 및 방향 전환 -> Slerp
//3. 자연스러운 카메라의 움직임 -> Slerp
// 요약: Lerp는 선형 보간을 사용하여 두 점 사이를 직선으로 이동시키고, Slerp는 구면 보간을 사용하여 두 점 사이를 곡선으로 이동시킵니다.
// Lerp: 체력 게이지 같은 선형 보간이 필요한 경우에 사용합니다.
// Slerp: 회전이나 방향 전환이 필요한 경우에 사용합니다.3D 회전(Quaternion), 벡터 간의 곡선 경로 확인, 방향 전환이 부드럽게 대상을 바라보도록 할 때 사용합니다.
public class SphericalInter : MonoBehaviour
{
    public Transform target;
    public float speed = 1.0f;

    private Vector3 startPosition;
    private float t = 0;

    private void Start()
    {
        // 시작 위치를 현재 위치로 설정
        startPosition = transform.position;
    }

    private void Update()
    {
        if (t < 1.0f)
        {
            // t 값을 증가시켜 보간을 진행
            t += Time.deltaTime * speed;
            // 보간된 위치 계산
            Vector3 newPosition = Vector3.Slerp(startPosition, target.position, t);
            // 오브젝트 위치 업데이트
            transform.position = newPosition;
        }
    }
}
