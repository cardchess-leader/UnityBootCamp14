using UnityEngine;

// 플레이어를 45도 방향으로 직진하는 코드

public class Anglemove : MonoBehaviour
{
    [SerializeField] 
    float angle_degree; // 이동할 각도 (도 단위)
    [SerializeField]
    float speed; // 이동 속도

    // Update is called once per frame
    void Update()
    {
        var radian = angle_degree * Mathf.Deg2Rad; // 각도를 라디안으로 변환
        Vector3 dir = new Vector3(Mathf.Cos(radian), 0, Mathf.Sin(radian)); // 이동 벡터 계산

        transform.Translate(dir * speed * Time.deltaTime, Space.World); // 월드 좌표계에서 이동

        if (Input.GetKeyDown(KeyCode.Return)) // Enter 키를 눌렀을 때
        {
            transform.position = Vector3.zero; // 오브젝트를 원점으로 이동
        }
    }
}
