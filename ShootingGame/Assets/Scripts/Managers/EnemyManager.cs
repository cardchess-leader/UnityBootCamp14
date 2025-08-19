using UnityEngine;

// ��ǥ: ���� �ð����� ���� ������ �� ��ġ�� ���� ��.
// �ʿ��� ������: ���� �ð�, ���� �ð�, �� ���� ����
// �۾� ����: 1. �ð��� üũ�ϰ� 2. �ð��� �Ǹ� ���� �����ϰ�(��Ÿ��) 3. ���� �� ��ġ�� ����

public class EnemyManager : MonoBehaviour
{
    float min = 1, max = 5; // ��ȯ �ð� ����(�ִ� �ּ�)
    float timeSinceLastSpawn = 0;
    public float coolTime;
    public GameObject enemyPrefab;
    public GameObject spawnArea;


    private void Start()
    {
        coolTime = Random.Range(min, max) * coolTime;
        ScoreManager.Instance.OnStageClear.AddListener(DisableEnemyManager);
    }

    void DisableEnemyManager() // �������� Ŭ���� �� �� ���� ����
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= coolTime)
        {
            var enemy = Instantiate(enemyPrefab, spawnArea.transform.position, enemyPrefab.transform.rotation);
            timeSinceLastSpawn = 0; // ��Ÿ�� �ʱ�ȭ
            coolTime = Random.Range(min, max); // ���� ��ȯ �ð� ������ �������� ����
        }
    }
}
