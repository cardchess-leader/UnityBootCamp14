using UnityEngine;

// 에디터 모드 상태에서 Update, OnEnable, OnDisable 등을 실행할 수 있도록 합니다.
// Play를 하지 않고도 MonoBehaviour의 메서드를 실행할 수 있습니다.
[ExecuteInEditMode]
public class EditMenuSample : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 에디터 모드에서만 실행되는 코드
        if (!Application.isPlaying)
        {
            Vector3 pos = transform.position;
            pos.y = 0; // y축을 0으로 고정
            transform.position = pos;
            Debug.Log("Editor Test...(이 스크립트를 낀 오브젝트는 y축이 0으로 고정됩니다.");
        }
    }
}
