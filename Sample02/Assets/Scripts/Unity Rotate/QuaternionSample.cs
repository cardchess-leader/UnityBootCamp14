using UnityEngine;

// 쿼터니언 기능 정리
// Quaternion.identity : 단위 쿼터니언, 회전이 없는 상태
// Quaternion.Euler(x, y, z) : 오일러 각을 사용하여 쿼터니언 생성
// Quaternion.AngleAxis(angle, axis) : 주어진 각도와 축을 사용하여 쿼터니언 생성
// Quaternion.Lerp(a, b, t) : a에서 b로 선형 보간하는 쿼터니언 생성
// Quaternion.Slerp(a, b, t) : a에서 b로 구면 선형 보간하는 쿼터니언 생성
// Quaternion.Inverse(q) : 주어진 쿼터니언의 역을 계산
// Quaternion.LookRotation(forward, up) : 주어진 방향(forward)과 위쪽(up)을 사용하여 쿼터니언 생성
// Quaternion.Dot(a, b) : 두 쿼터니언의 내적 계산
// Quaternion.Normalize(q) : 주어진 쿼터니언을 정규화
// Quaternion.Angle(a, b) : 두 쿼터니언 사이의 각도 계산
// Quaternion.ToEulerAngles() : 쿼터니언을 오일러 각으로 변환
// forward: 회전시킬 방향
// up: 회전시킬 위쪽 방향

// 회전 값 적용
// transform.rotation = Quaternion.Euler(0, 90, 0); // 현재 오브젝트의 회전 값을 적용합니다.

// 회전에 대한 보간
// Quaternion.Lerp(a, b, t) : a에서 b로 선형 보간하는 쿼터니언 생성
// Quaternion.Slerp(a, b, t) : a에서 b로 구면 선형 보간하는 쿼터니언 생성
// Quaternion.RotateTowards(from, to, maxDegreesDelta) : from에서 to로 회전하는 쿼터니언 생성

public class QuaternionSample : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

// transform.LookAt(target); // 타겟을 바라보는 회전 적용

// transform.LookRotation(Vector3.forward, Vector3.up); // 특정 방향을 바라보는 회전 적용