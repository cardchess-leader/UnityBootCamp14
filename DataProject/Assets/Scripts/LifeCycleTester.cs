using System.Collections;
using UnityEngine;

// 유니티 라이프 싸이클에 대한 동작 순서 확인 코드
// Update를 활용해 프레임 별 호출을 순서대로 확인해봅니다.

public class LifeCycleTseter : MonoBehaviour
{
    private int count_per_frame = 0; // 프레임 단위 호출 카운트
    private void Awake()
    {
        Debug.Log("[Awake] 오브젝트의 생성 시 딱 한번만 실행되는 영역");
    }

    private void OnEnable()
    {
        Debug.Log("[OnEnable] 오브젝트가 활성화 될 때마다 실행되는 영역");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("[Start] 첫 프레임 시작 전에 호출되는 영역");
        StartCoroutine(CustomCoroutine()); // 커스텀 코루틴 시작
    }

    private void FixedUpdate()
    {
        Debug.Log($"[CPF : {count_per_frame}] [FixedUpdate] 물리에 대한 업데이트가 진행되는 영역");
    }

    // Update is called once per frame
    void Update()
    {
        count_per_frame++;
        Debug.Log($"[CPF : {count_per_frame}] [Update] 게임 로직에 대한 호출이 진행되는 영역");

        if(count_per_frame == 3)
        {
            Debug.Log($"[CPF : {count_per_frame}] [Test 1]오브젝트의 비활성화 작업을 진행합니다.");
            gameObject.SetActive(false); // 오브젝트 비활성화
        }

        if (count_per_frame == 6)
        {
            Debug.Log($"[CPF : {count_per_frame}] [Test 2]오브젝트의 활성화 작업을 진행합니다.");
            gameObject.SetActive(true); // 오브젝트 활성화
        }

        if (count_per_frame == 9)
        {
            Debug.Log($"[CPF : {count_per_frame}] [Test 2]오브젝트의 파괴 작업을 진행합니다.");
            Destroy(gameObject); // 오브젝트 파괴
        }
    }

    private void LateUpdate()
    {
        Debug.Log($"[CPF : {count_per_frame}] [LateUpdate] 물리 연산, 카메라 연산 등 후처리 작업이 진행되는 영역 (로직 이후의 후처리)");
    }

    // 코루틴 설계)
    // yield에 의해 대기 후 싸이클로 돌아오는 과정이 존재하며, 보통 Update의 틈새에 맞춰져 실행됩니다.
    IEnumerator CustomCoroutine()
    {
        Debug.Log("[Coroutine] 커스텀 코루틴이 시작되었습니다.");
        yield return null;
        Debug.Log("[Coroutine] 첫 번째 yield 후에 호출되는 영역");

        yield return new WaitForSeconds(1); // 2초 
        Debug.Log("[Coroutine] 1초 후에 호출되는 영역");

        yield return new WaitForFixedUpdate(); // FixedUpdate가 끝날 때까지 대기
        Debug.Log("[Coroutine] FixedUpdate가 끝난 후에 호출되는 영역");

        yield return new WaitForEndOfFrame(); // 프레임의 마지막에 호출
        Debug.Log("[Coroutine] 프레임의 마지막에 호출되는 영역");
    }

    private void OnDisable()
    {
        Debug.Log("[OnDisable] 오브젝트가 비활성화 될 때마다 실행되는 영역");
    }

    private void OnDestroy()
    {
        Debug.Log("[OnDestroy] 오브젝트가 파괴될 때마다 실행되는 영역");
        // 이 위치에서는 파괴 절차가 진행되고 있기 때문에, 다음과 같은 작업은 불가능합니다.
        // gameObject.SetActive(false); // 오브젝트 비활성화
        // 자기 자신에 대한 복구 작업은 불가능합니다.
        // 새로운 게임 오브젝트를 생성하는 등의 작업은 가능합니다.
    }
}
