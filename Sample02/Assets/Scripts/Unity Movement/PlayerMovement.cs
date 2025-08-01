using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jp = 5f;
    public LayerMask ground;

    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearDamping = 0;
    }

    // Update is called once per frame
    void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        // 방향 설계
        Vector3 dir = new Vector3(x, 0, y).normalized; // 입력 방향 벡터

        // 이동 속도 설정
        Vector3 velocity = dir * speed;

        rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
        // 리지드 바디의 속성
        // linearVelocity : 선형 속도(물체가 공간 상에서 이동하는 속도)
        // angularVelocity : 회전 속도(물체가 회전하는 속도)

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            rb.AddForce(Vector3.up * jp, ForceMode.Impulse); // 점프
            // ForceMode.Impulse : 순간적인 힘을 가함
            // ForceMode.Force : 지속적인 힘을 가함
        }
    }

    bool IsGrounded
    {
        get
        {
            // 아래 방향으로 1만큼 레이를 쏴서 레이어 체크
            return Physics.Raycast(transform.position, Vector3.down, 1f, ground);
        }
    }
}
