using UnityEngine;

// 오브젝트를 회전시키는 MonoBehaviour 스크립트
// 유니티의 회전 (Rotate)
// 1. 오일러 각(Euler Angles)에 의한 회전 - x, y, z 축을 기준으로 회전
// 유니티 인스펙터에서의 Transform 컴포넌트의 Rotation에 표기된 값(각도 기준)
// ex) Rotation X 120 Y 45 Z 0 -> X축으로 120도, Y축으로 45도 회전
// 2. 쿼터니언(Quaternion)에 의한 회전 - 회전을 수학적으로 표현하는 방법으로, 회전의 누적을 쉽게 처리할 수 있음
// 유니티 엔진 내부적으로는 쿼터니언을 사용하여 회전을 처리
// 쿼터니언으로 처리하는 이유
// - 오일러 각은 짐벌락(Gimbal Lock) 문제를 일으킬 수 있음
// 유니티에서 쿼터니언 -> 오일러 각 변환 시 360도 이상의 회전은 보정될 수 있음.
// 380도 회전 -> 20도 회전으로 보정됨.

// 짐벌 락 현상(Gimbal Lock) : 오일러 각을 이용해 회전을 표현하는 경우에 발생하는 회전 자유도의 손실 현상
// -> 어떤 축이 다른 축과 정렬되는 순간, 한 축의 회전이 무효화되면서 회전 방향이 3개가 아닌 2개로 제한됨
// 쿼터니언의 구조
// - 4개의 값으로 구성됨 (x, y, z, w)
// - x, y, z는 회전 축을 나타내고, w는 회전의 크기를 나타냄
// - 쿼터니언은 회전을 표현하는데 있어 짐벌 락 문제를 피할 수 있는 장점이 있음
// 쿼터니언도 벡터처럼 방향인 동시에 회전의 개념을 가지고 있습니다. 
// 세 축을 동시에 회전시켜 짐벌 락 현상이 발생하지 않도록 구성되어 있음 (x, y, z 축은 항상 동시에 변화한다)
// orientation : 방향

// 단점 : 180도 이상의 표현 불가
// 직관적으로 바로 이해하기 어려운 구조

public class ObjectRotate : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // transform.Rotate()는 회전을 진행시키는 가장 기본적인 코드
        // transform.Rotate(Vector3 eulerAngles) : 오일러 각을 사용하여 회전
        // transform.Rotate(float x, float y, float z) : 각각의 축에 대한 회전 각도를 지정
        // transform.Rotate(Vector3 axis, float angle) : 특정 축(axis)을 기준으로 회전 각도(angle)를 지정

        // 월드 기준으로 z축으로 60도 회전
        // ex) transform.Rotate(Vector3.forward, 60f * Time.deltaTime, Space.World);

        // 게임 오브젝트를 기준으로 회전을 진행합니다.
        transform.Rotate(new Vector3(20, 20, 20) * Time.deltaTime);
        // 월드 좌표를 기준으로 회전을 진행합니다.
        transform.Rotate(new Vector3(20, 20, 20) * Time.deltaTime, Space.World);
    }
}
