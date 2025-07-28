using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class VectorTest1 : MonoBehaviour
{
    // 게임 오브젝트의 Transform을 통해 Vector 값 구하기
    public Transform A_CUBE;
    public Transform B_CUBE;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 현재 큐브의 위치를 벡터로 설정합니다.
        Vector3 posA = A_CUBE.position; // A_CUBE의 위치를 가져옵니다.
        Vector3 posB = B_CUBE.position; // B_CUBE의 위치를 가져옵니다.

        Vector3 atob = posB - posA; // A_CUBE에서 B_CUBE로 향하는 방향 벡터를 계산합니다.
        Vector3 btoa = posA - posB; // B_CUBE에서 A_CUBE로 향하는 방향 벡터를 계산합니다.

        float distance = Vector3.Distance(posA, posB); // 두 큐브 사이의 거리를 계산합니다.
        Debug.Log($"Distance from A to B: {distance}"); // 거리 출력
        // 거리 측정
        // Distance의 수학적 로직
        // a = (ax, ay, az)
        // b = (bx, by, bz)
        // 두 벡터 사이의 거리는
        // 각 축에 대한 차의 제곱의 합에 대한 제곱근을 구합니다.
        // distance = √((bx - ax)² + (by - ay)² + (bz - az)²)
        // 유니티의 Mathf 클래스 기반으로 위 식을 바꾸면?
        // distance = Mathf.Sqrt(Mathf.Pow(btoa.x, 2) + Mathf.Pow(btoa.y, 2) + Mathf.Pow(btoa.z, 2));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
