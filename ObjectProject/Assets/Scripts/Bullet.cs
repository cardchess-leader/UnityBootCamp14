using System.Collections.Generic;
using System.Collections;
using UnityEngine;

// �Ѿ˿� ���� ����, �Ѿ� �ݳ�, �Ѿ� �̵�
public class Bullet : MonoBehaviour
{
    public float speed = 20f; // �Ѿ� �ӵ�
    public float lifeTime = 2f; // �Ѿ� ���� �ð�
    public int damage; // �Ѿ��� ������ ������
    public float criChance = 0.1f; // ũ��Ƽ�� Ȯ��
    public List<GameObject> effectPrefabs;

    BulletPool pool; // �Ѿ� Ǯ ����
    Coroutine lifeCoroutine;

    private void OnEnable()
    {
        lifeCoroutine = StartCoroutine(LifeCoroutine());
    }

    private void OnDisable()
    {
        if (lifeCoroutine != null)
        {
            StopCoroutine(lifeCoroutine);// �ڷ�ƾ ����
            lifeCoroutine = null; // ���� ����
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime); // �Ѿ� �̵�
    }

    IEnumerator LifeCoroutine()
    {
        yield return new WaitForSeconds(lifeTime); // ���� �ð� ���
        ReturnPool(); // ���� �ð��� ������ �Ѿ��� Ǯ�� ��ȯ
    }

    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ������Ʈ�� Enemy �±׸� ���� ���
        if (other.CompareTag("Enemy"))
        {
            bool isCritical = false;
            // 10% Ȯ���� ũ��Ƽ�� ��Ʈ �߻�
            if (Random.Range(0f, 1f) < criChance)
            {
                isCritical = true;
            }
            other.GetComponent<EnemyController>()?.TakeDamage(isCritical ? int.MaxValue : damage); // EnemyHealth ������Ʈ���� ������ ó��

            // effectPrefabs�� ũ�Ⱑ 2��
            // 0�� �ε����� �Ϲ� ����Ʈ, 1�� �ε����� ũ��Ƽ�� ����Ʈ
            int idx = isCritical ? 1 : 0; // ũ��Ƽ�� ���ο� ���� ����Ʈ �ε��� ����
            // �ش� �ε����� ����Ʈ �������� �ν��Ͻ�ȭ
            if (effectPrefabs == null || effectPrefabs.Count <= idx)
            {
                Debug.LogWarning("Effect prefabs not set or index out of range.");
                return;
            }
            Instantiate(effectPrefabs[idx], transform.position, Quaternion.identity);
        }

        ReturnPool(); // �浹 �� �Ѿ��� Ǯ�� ��ȯ
    }

    public void SetPool(BulletPool bulletPool)
    {
        this.pool = bulletPool; // �Ѿ� Ǯ ����
    }

    void ReturnPool() => pool.ReturnBullet(gameObject);
}
