using UnityEngine;
using UnityEngine.InputSystem;

// Player Input�� �����ؾ� ��� ����

// RequireComponent(typeof(T))�� �� ��ũ��Ʈ�� ������Ʈ�� ����� ��� �ش� ������Ʈ��
// �ݵ�� T�� ������ �־�� �մϴ�. ���� ����� �ڵ����� ������ְ�,
// �� �ڵ尡 �����ϴ� �� �����Ϳ��� ���� ������Ʈ�� �ش� ������Ʈ�� ������ �� �����ϴ�.
[RequireComponent(typeof(PlayerInput))]
public class InputSystemExample : MonoBehaviour
{
    // ���� Action Map: Sample
    // Action: Move
    // Type: Value
    // Composite Type: Vector2
    // Binding: 2D Vector(WASD)
    public float speed = 1.0f;
    Vector2 moveInputValue;
    // Send Message�� ���Ǵ� ���
    // Ư�� Ű�� ������, Ư�� �Լ��� ȣ���մϴ�.
    // �Լ� ���� On + Actions name, ���� ���� Actions�� �̸��� Move���
    // �Լ����� OnMove�� �˴ϴ�.
    void OnMove(InputValue value)
    {
        moveInputValue = value.Get<Vector2>();
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(moveInputValue.x, 0, moveInputValue.y);
        transform.Translate(move * speed * Time.deltaTime, Space.World);
    }
}

// (0, 0, 0)�� ���� �߽���
// X : ��, ��
// Y : ��, �Ʒ�
// Z : ��, ��


// ===================================================================================
// ���� ��ǥ��(World Space)
// ���� ��ǥ��(Local Space)
// ���� ��ǥ��(Local Space)
// Ư�� ������Ʈ ������ ��ǥ
// ������Ʈ�� ��ġ, ȸ��, ũ�⸦ �������� ��ǥ�� ����
// ������Ʈ�� ���⿡ ���� �����̰� ��.

