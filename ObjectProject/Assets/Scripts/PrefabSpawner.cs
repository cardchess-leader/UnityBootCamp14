using UnityEngine;
// 1. �������� ���� �Ҵ��Ͽ� �����ϴ� MonoBehaviour ��ũ��Ʈ
// 2. ������ ������Ʈ�� ���� ������ ���ο��� ������.
// 3. ���� �Ŀ� �ı��� ���� ������ �ð��� ������.

// �ش� ��ũ��Ʈ�� ���� ������Ʈ�� ����Ǹ�, ������ �����ϰ� ���� �ð� �Ŀ� �ı��ϴ� ����� �����Ѵ�.
// ����) �������� ����� �Ǿ� �־�� �ϸ�, ��ϵ��� ���� ��� ��� �޽����� ����Ѵ�.

public class PrefabSpawner : MonoBehaviour
{
    public GameObject prefab; // The prefab to spawn
    GameObject spawnedObject; // The spawned object reference
    public float delay = 5f; // Delay before destroying the spawned object

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (prefab != null)
        {
            spawnedObject = Instantiate(prefab);
            // ���� �ڵ� Instantiate()
            // 1. Instantiate(prefab); �ش� �������� ������ �°� ��ġ�� ȸ���� �����Ͽ� ����
            // 2. Instantiate(prefab, transform.position, Quaternion.Identity); ���� ������Ʈ�� ��ġ�� ȸ���� �������� ����
            // 3. Instantiate(prefab, parent); �θ� ������Ʈ�� �����Ͽ� ����
            // 4. Instantiate(prefab, transform.position, transform.rotation, parent); ���� ������Ʈ�� ��ġ�� ȸ���� �������� �θ� ������Ʈ�� �����Ͽ� ����
            // ������Ʈ�� �����ϰ�, �� ������Ʈ�� ������ ��ġ�� �ڽ����ν� ����մϴ�.
            Debug.Log(spawnedObject.name + "�� �����Ǿ����ϴ�.");
            spawnedObject.name = "������ ������Ʈ"; // ������ ������Ʈ�� �̸��� �����մϴ�.
            //spawnedObject.AddComponent<Rigidbody>(); // ������ ������Ʈ�� Rigidbody ������Ʈ�� �߰��մϴ�.
            Destroy(spawnedObject, delay); // ���� �ð� �Ŀ� ������ ������Ʈ�� �ı��մϴ�.
        }
        else
        {
            Debug.LogWarning("��ϵ� �������� ���� �����ϴ�.");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
