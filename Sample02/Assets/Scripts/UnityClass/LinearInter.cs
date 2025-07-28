using UnityEngine;

// 선형 보간을 위한 Unity MonoBehaviour 클래스
public class LinearInter : MonoBehaviour
{
    // Vector3.Lerp(start, end, t) 함수를 사용하여 선형 보간을 수행합니다.
    // start -> end까지 t에 따라 선형 보간합니다.
    // -> 해당 방향으로 일정 간격으로 천천히 이동합니다.
    // t는 0에서 1 사이의 값으로, 0이면 start 위치, 1이면 end 위치에 해당합니다.
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
            Vector3 newPosition = Vector3.Lerp(startPosition, target.position, t);
            // 오브젝트 위치 업데이트
            transform.position = newPosition;
        }
    }
}
