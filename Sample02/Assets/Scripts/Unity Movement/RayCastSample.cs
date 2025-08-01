using UnityEngine;

// Raycast : 시작 위치에서 특정 방향으로 광선을 쏴서 충돌하는 오브젝트를 감지하는 기능
// 특정 오브젝트를 충돌 범위에서 제외하는 작업 가능
// 특정 오브젝트에 대한 충돌 판정을 확인하는 용도

public class RayCastSample : MonoBehaviour
{
    RaycastHit hit; // RaycastHit 구조체는 광선이 충돌한 오브젝트에 대한 정보를 담고 있음

    // ref : 변수를 참조로 전달
    // out : 변수를 참조로 전달하고, 변수 전달 전에 초기화를 진행할 필요가 없음
    // Physics.Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance = Mathf.Infinity, int layerMask = Physics.DefaultRaycastLayers)

    // origin 방향에서 direction 방향으로 maxDistance 거리만큼 광선을 쏘고, 충돌한 오브젝트에 대한 정보를 hitInfo에 저장
    // hitInfo는 충돌체에 대한 정보를 담고 있는 RaycastHit 구조체

    const float length = 20f; // 광선의 길이
    int layerMask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        // 한 번에 여러개의 레이캐스트 충돌 처리

        // 선 그리기
        Debug.DrawRay(transform.position, transform.forward * length, Color.red); // 광선을 시각적으로 표시

        // 레이어 마스크 설정하기
        // 1. 충돌시키고 싶지 않은 레이어에 대한 변수 설정
        int ignoreLayer = LayerMask.NameToLayer("Red"); // 충돌시키고 싶지 않은 레이어
        layerMask = ~(1 << ignoreLayer);

        // 충돌체 설정(묶음)
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, length, layerMask);
        // RaycastAll : 한 방향으로 쏜 레이가 충돌한 충돌체를 배열로 return

        // 반복문으로 콜라이더 체크
        foreach (RaycastHit hit in hits)
        {
            Debug.Log(hit.collider.name + " was hit!");
            hit.collider.gameObject.SetActive(false); // 맞은 충돌체의 게임오브젝트 비활성화
        }
    }

    // Update is called once per frame
    void Update()
    {
        return;
        // 왼쪽 마우스 버튼을 클릭했을 때
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, length, layerMask))
            {
                Debug.Log("히히히 발싸!");
                Debug.Log(hit.collider.name);
                hit.collider.gameObject.SetActive(false); // 충돌한 오브젝트를 비활성화

                // 레이어마스크는 비트마스크이며, 각 비트는 하나의 레이어를 의미합니다.
                // 1 << n은 n번째 레이어만 포함하는 마스크를 의미합니다.
                // ~에 의해 작성된 ~(1 << n)은 n번째 비트를 제외한 비트마스크를 생성합니다.
            }
        }

        // 오브젝트의 위치에서 정면으로 length만큼의 길이에 해당하는 디버깅 광선을 쏘는 코드
        // 주로 레이케스트 작업에서 시각적으로 확인하기 위해 사용
        Debug.DrawRay(transform.position, transform.forward * length, Color.red); // 광선을 시각적으로 표시
    }
}
