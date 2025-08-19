using UnityEngine;
// �÷��̾�� ���� ����
// �÷��̾�� ��Ʈ���� ����ڰ� �����մϴ�.
// ���� AI�� ��Ʈ���� �����մϴ�.
public class Enemy : MonoBehaviour
{
    public enum EnemyType
    {
        Down, // �Ʒ��� �������� �� 
        Chase // �÷��̾ �����ϴ� ��
    }
    public float speed = 5f; // ���� �̵� �ӵ�
    public EnemyType enemyType; // ���� ����
    Vector3 dir;

    // ���� ����
    void Start()
    {
        PatternSetting(); // ���� ���� �޼ҵ� ȣ�� 
    }

    private void PatternSetting()
    {
        int rand = Random.Range(0, 10);
        if (rand < 3) // 30% Ȯ���� Chase Ÿ������ ����
        {
            GameObject target = GameObject.FindGameObjectWithTag("Player"); // �÷��̾� ������Ʈ�� ã���ϴ�.
            dir = (target.transform.position - transform.position).normalized; // �÷��̾� ������ ����մϴ�.
            enemyType = EnemyType.Chase; // Chase Ÿ������ ����
        }
        else // ������ 70%�� Down Ÿ������ ����
        {
            dir = Vector3.down; // �Ʒ��� �������� �������� ����
            enemyType = EnemyType.Down; // Down Ÿ������ ����
        }
    }

    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        // �浹�� ������Ʈ�� �±װ� "Player"�� ���
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject); // �÷��̾� ������Ʈ ����
            Destroy(gameObject); // �� ������Ʈ ����
        } else
        {
            Destroy(gameObject);
        }
    }
}