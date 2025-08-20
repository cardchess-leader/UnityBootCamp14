using UnityEngine;

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
            enemyType = EnemyType.Chase; // Chase Ÿ������ ����
        }
        else // ������ 70%�� Down Ÿ������ ����
        {
            enemyType = EnemyType.Down; // Down Ÿ������ ����
        }
    }

    void Update()
    {
        var dir = Vector3.down;
        if (enemyType == EnemyType.Chase) {
            GameObject target = GameObject.FindGameObjectWithTag("Player");
            dir = (target.transform.position - transform.position).normalized;
        }
        transform.position += dir * speed * Time.deltaTime;
        // also make the enemy to face the direction of movement
        transform.rotation = Quaternion.LookRotation(Vector3.back, -dir);
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