using System.Collections.Generic;
using System.Collections;
using UnityEngine;

// �Ѿ˿� ���� ����, �Ѿ� �ݳ�, �Ѿ� �̵�
public class Bullet : MonoBehaviour
{
    public float speed = 20f; // �Ѿ� �ӵ�
    public float lifeTime = 2f; // �Ѿ� ���� �ð�
    public int damage = 1; // �Ѿ��� ������ ������
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
            other.GetComponent<EnemyController>()?.TakeDamage(damage); // EnemyHealth ������Ʈ���� ������ ó��
        }
        // �ε�ģ ������Ʈ�� Enemy �±׸� ���� ���
        // �������� �����ϴ�.
        // ����Ʈ ����(��ƼŬ)
        if (effectPrefabs != null && effectPrefabs.Count > 0)
        {
            int idx = Random.Range(0, effectPrefabs.Count);
            var effectPrefab = effectPrefabs[idx];
            if (effectPrefab != null)
                Instantiate(effectPrefab, transform.position, Quaternion.identity);
        }

        ReturnPool(); // �浹 �� �Ѿ��� Ǯ�� ��ȯ
    }

    public void SetPool(BulletPool bulletPool)
    {
        this.pool = bulletPool; // �Ѿ� Ǯ ����
    }

    void ReturnPool() => pool.ReturnBullet(gameObject);
}
