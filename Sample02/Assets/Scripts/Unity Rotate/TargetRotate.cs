using UnityEngine;

// 목표 방향으로 회전하는 MonoBehaviour 스크립트
public class TargetRotate : MonoBehaviour
{
    public Transform target; // 회전할 대상의 Transform
    public float rotationSpeed = 90f; // 회전 속도
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Quaternion.LookRotation(Vector3 forward); 특정 방향을 바라보는 회전을 계산
        var targetRotation = Quaternion.LookRotation(target.position - transform.position);
        // 현재의 회전에서 타겟의 회전으로 일정 속도로 부드럽게 회전을 진행하는 함수
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
