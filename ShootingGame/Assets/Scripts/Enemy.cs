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
    public int score = 5; // ���� óġ���� �� �־����� ����
    Vector3 dir;

    public GameObject explosionPrefab;

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
        ScoreManager.Instance.AddScore(score); // ���� �߰�
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity); // ���� ����Ʈ ����
        collision.gameObject.SetActive(false); // �浹�� ������Ʈ ��Ȱ��ȭ
        gameObject.SetActive(false); // �ڽ�(enemy) ���ӿ�����Ʈ ��Ȱ��ȭ
        if (collision.gameObject.CompareTag("Player"))
        {
            ScoreManager.Instance.GameOver(); // �÷��̾�� �浹 �� ���� ���� ó��
        }
    }
}