using UnityEngine;

// 삼각 함수
// 유니티에서 제공해주는 삼각함수는 주로 회전, 카메라 제어, 곡선, 움직임에 대한 표현으로 사용됩니다.
// 특징) 단위를 라디안으로 사용합니다.

public class Tfunction : MonoBehaviour
{
    // 요약
    // Sin(Radian) : 주어진 각도의 Y좌표를 반환합니다.
    // Cos(Radian) : 주어진 각도의 X좌표를 반환합니다.
    // Tan(Radian) : 주어진 각도의 Y/X 비율을 반환합니다.(기울기)
    Vector3 pos;
    //float time;

    public void CircleRotate()
    {
        float angleVelocity = 90.0f; // 회전 속도 (도 단위)
        float radius = 5.0f; // 원의 반지름
        float angle = Time.time * angleVelocity; // 회전 속도 설정
        float radian = angle * Mathf.Deg2Rad; // 각도를 라디안으로 변환

        var x = Mathf.Cos(radian) * radius; // Cosine을 사용하여 X좌표 계산
        var y = Mathf.Sin(radian) * radius; // Sine을 사용하여 Y좌표 계산

        transform.position = new Vector3(x, y, 0); // 원을 그리며 이동
    }

    public void ButterFly()
    {
        float t = Time.time * 2f;
        float x = Mathf.Sin(t) * 4;
        float y = Mathf.Sin(t * 2f) * 2;

        transform.position = new Vector3(x, y, 0); // 나비 효과를 주며 이동
    }

    public void Wave()
    {
        var offset = Mathf.Sin(Time.time * 12.3f) * 3f; // Sin 함수를 사용하여 Y좌표에 파동 효과를 적용
        var offset2 = Mathf.Cos(Time.time * 9.9f) * 7f; // Sin 함수를 사용하여 Y좌표에 파동 효과를 적용
        transform.position = pos + Vector3.up * offset + Vector3.right * offset2; // 원래 위치에 파동 효과를 적용하여 이동
    }

    void Start()
    {
        pos = transform.position; // 시작할 때 오브젝트의 현재 위치를 저장
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) // 마우스 왼쪽 버튼 클릭 시
        {
            CircleRotate(); // 원형 회전 함수 호출
        }
        else if (Input.GetMouseButton(1)) // 마우스 오른쪽 버튼 클릭 시
        {
            Wave(); // 파동 효과 함수 호출
        } else if (Input.GetMouseButton(2)) // 다른 키 입력이 있을 때
        {
            ButterFly(); // 나비 효과 함수 호출
        }
    }
}
