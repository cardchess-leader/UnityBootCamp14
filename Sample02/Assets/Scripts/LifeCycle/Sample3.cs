using UnityEngine;

// 캐시(Cache)?
// 자주 사용되는 데이터나 값을 미리 복사해두는 임시 장소

public class Sample3 : MonoBehaviour
{
    Rigidbody rb;
    Vector3 velocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // 캐싱(caching): GetComponent를 Start에서 한 번만 호출하여 성능을 향상시킵니다.
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().AddForce(velocity * 5); // 매 프레임마다 Rigidbody에 힘을 추가합니다.
    }
}
