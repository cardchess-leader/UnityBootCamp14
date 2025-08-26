using UnityEngine;

// 1. The missile cannot be launched if the target is null.
// 2. If the target is destroyed or there is no target, the missile flies straight forward from its current direction.

public class Missile : MonoBehaviour
{
    [SerializeField]
    float acceleration;
    [SerializeField]
    float maxSpeed;
    [SerializeField]
    float turnSpeed = 5f; // 회전 속도
    [SerializeField]
    GameObject explosionEffect;
    
    float initialSpeed;
    float currentSpeed;
    private Enemy target;
    private bool hasTarget = false;

    private void Start()
    {
        // Destroy after 10 seconds to avoid clutter
        Destroy(gameObject, 10f);
        currentSpeed = initialSpeed;
    }
    
    void Update()
    {
        // 타겟이 있고 타겟이 살아있으면 추적
        if (hasTarget && target != null)
        {
            TrackTarget();
        }
        
        // 앞으로 이동
        transform.position += transform.forward * currentSpeed * Time.deltaTime;
        currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.deltaTime, maxSpeed);
    }
    
    private void TrackTarget()
    {
        // 타겟 방향 계산
        Vector3 targetDirection = (target.transform.position - transform.position).normalized;
        
        // 현재 방향에서 타겟 방향으로 부드럽게 회전
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Destroy the missile on impact
        Destroy(gameObject);
        // Optionally, instantiate explosion effect here
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
    }

    public void SetInitSpeed(float initialSpeed)
    {
        this.initialSpeed = Mathf.Min(initialSpeed + 10, maxSpeed); // Add a small boost to the initial speed
    }
    
    public void SetTarget(Enemy enemy)
    {
        target = enemy;
        hasTarget = (target != null);
    }
}
