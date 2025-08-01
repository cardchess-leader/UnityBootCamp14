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

        // ���� ����
        Vector3 dir = new Vector3(x, 0, y).normalized; // �Է� ���� ����

        // �̵� �ӵ� ����
        Vector3 velocity = dir * speed;

        rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
        // ������ �ٵ��� �Ӽ�
        // linearVelocity : ���� �ӵ�(��ü�� ���� �󿡼� �̵��ϴ� �ӵ�)
        // angularVelocity : ȸ�� �ӵ�(��ü�� ȸ���ϴ� �ӵ�)

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            rb.AddForce(Vector3.up * jp, ForceMode.Impulse); // ����
            // ForceMode.Impulse : �������� ���� ����
            // ForceMode.Force : �������� ���� ����
        }
    }

    bool IsGrounded
    {
        get
        {
            // �Ʒ� �������� 1��ŭ ���̸� ���� ���̾� üũ
            return Physics.Raycast(transform.position, Vector3.down, 1f, ground);
        }
    }
}
