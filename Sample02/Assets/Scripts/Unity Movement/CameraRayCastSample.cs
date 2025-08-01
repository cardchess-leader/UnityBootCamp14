using UnityEngine;
// 카메라 기준으로 마우스 클릭 위치에 레이캐스트 처리

public class CameraRayCastSample : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Ray ray = new Ray(위치, 방향)
            // 카메라에서 사용할 레이를 따로 설정
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // 충돌한 오브젝트의 이름을 출력
                Debug.Log("Hit: " + hit.collider.name);

                // 랜덤 컬러 생성
                Color randomColor = new Color(Random.value, Random.value, Random.value);
                hit.collider.GetComponent<Renderer>().material.color = randomColor;
                // 
                var hitObject = hit.collider.gameObject;
                int change_layer = LayerMask.NameToLayer("Yellow");
                if (change_layer != -1)
                {
                    hitObject.layer = LayerMask.NameToLayer("Yellow");
                }
            }
            else
            {
                Debug.Log("No hit detected.");
            }
        }
    }
}
