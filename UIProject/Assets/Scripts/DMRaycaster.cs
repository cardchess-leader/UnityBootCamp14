using UnityEngine;

// 마우스 충돌 시의 대화 시작 가능 (카메라에 연결)
public class DMRaycaster : MonoBehaviour
{
    public float rayLength = 10f; // 레이의 길이
    public LayerMask layerMask; // 충돌을 감지할 레이어 마스크
    Camera mainCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main; // 메인 카메라를 가져옵니다.
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭 시
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); // 마우스 위치에서 레이를 생성

            if (Physics.Raycast(ray, out RaycastHit hit, rayLength, layerMask)) // 레이가 충돌하는지 확인
            {
                // 트리거 체크
                DTrigger dTrigger = hit.collider.GetComponent<DTrigger>(); // 충돌한 오브젝트에서 DTrigger 컴포넌트를 찾음
                if (dTrigger != null) // DTrigger가 존재하면
                {
                    dTrigger.OnDTriggerEnter(); // 대화를 시작합니다.
                }
            }
        }
    }
}
