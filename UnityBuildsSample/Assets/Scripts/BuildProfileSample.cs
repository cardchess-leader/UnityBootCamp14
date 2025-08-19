using UnityEngine;

public class BuildProfileSample : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

#if CUSTOM_DEBUG_MODE
        Debug.Log("디버그 모드에서 실행 중인 기능입니다.");
#elif CUSTOM_RELEASE_MODE // 위의 조건이 만족한다면 해당 위치의 기능은 비활성화됩니다.
        Debug.Log("릴리즈 모드에서 실행 중인 기능입니다.");
#endif
    }

    // Update is called once per frame+
    void Update()
    {
        
    }
}

