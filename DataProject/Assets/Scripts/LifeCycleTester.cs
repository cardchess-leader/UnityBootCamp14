using System.Collections;
using UnityEngine;

// ����Ƽ ������ ����Ŭ�� ���� ���� ���� Ȯ�� �ڵ�
// Update�� Ȱ���� ������ �� ȣ���� ������� Ȯ���غ��ϴ�.

public class LifeCycleTseter : MonoBehaviour
{
    private int count_per_frame = 0; // ������ ���� ȣ�� ī��Ʈ
    private void Awake()
    {
        Debug.Log("[Awake] ������Ʈ�� ���� �� �� �ѹ��� ����Ǵ� ����");
    }

    private void OnEnable()
    {
        Debug.Log("[OnEnable] ������Ʈ�� Ȱ��ȭ �� ������ ����Ǵ� ����");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("[Start] ù ������ ���� ���� ȣ��Ǵ� ����");
        StartCoroutine(CustomCoroutine()); // Ŀ���� �ڷ�ƾ ����
    }

    private void FixedUpdate()
    {
        Debug.Log($"[CPF : {count_per_frame}] [FixedUpdate] ������ ���� ������Ʈ�� ����Ǵ� ����");
    }

    // Update is called once per frame
    void Update()
    {
        count_per_frame++;
        Debug.Log($"[CPF : {count_per_frame}] [Update] ���� ������ ���� ȣ���� ����Ǵ� ����");

        if(count_per_frame == 3)
        {
            Debug.Log($"[CPF : {count_per_frame}] [Test 1]������Ʈ�� ��Ȱ��ȭ �۾��� �����մϴ�.");
            gameObject.SetActive(false); // ������Ʈ ��Ȱ��ȭ
        }

        if (count_per_frame == 6)
        {
            Debug.Log($"[CPF : {count_per_frame}] [Test 2]������Ʈ�� Ȱ��ȭ �۾��� �����մϴ�.");
            gameObject.SetActive(true); // ������Ʈ Ȱ��ȭ
        }

        if (count_per_frame == 9)
        {
            Debug.Log($"[CPF : {count_per_frame}] [Test 2]������Ʈ�� �ı� �۾��� �����մϴ�.");
            Destroy(gameObject); // ������Ʈ �ı�
        }
    }

    private void LateUpdate()
    {
        Debug.Log($"[CPF : {count_per_frame}] [LateUpdate] ���� ����, ī�޶� ���� �� ��ó�� �۾��� ����Ǵ� ���� (���� ������ ��ó��)");
    }

    // �ڷ�ƾ ����)
    // yield�� ���� ��� �� ����Ŭ�� ���ƿ��� ������ �����ϸ�, ���� Update�� ƴ���� ������ ����˴ϴ�.
    IEnumerator CustomCoroutine()
    {
        Debug.Log("[Coroutine] Ŀ���� �ڷ�ƾ�� ���۵Ǿ����ϴ�.");
        yield return null;
        Debug.Log("[Coroutine] ù ��° yield �Ŀ� ȣ��Ǵ� ����");

        yield return new WaitForSeconds(1); // 2�� 
        Debug.Log("[Coroutine] 1�� �Ŀ� ȣ��Ǵ� ����");

        yield return new WaitForFixedUpdate(); // FixedUpdate�� ���� ������ ���
        Debug.Log("[Coroutine] FixedUpdate�� ���� �Ŀ� ȣ��Ǵ� ����");

        yield return new WaitForEndOfFrame(); // �������� �������� ȣ��
        Debug.Log("[Coroutine] �������� �������� ȣ��Ǵ� ����");
    }

    private void OnDisable()
    {
        Debug.Log("[OnDisable] ������Ʈ�� ��Ȱ��ȭ �� ������ ����Ǵ� ����");
    }

    private void OnDestroy()
    {
        Debug.Log("[OnDestroy] ������Ʈ�� �ı��� ������ ����Ǵ� ����");
        // �� ��ġ������ �ı� ������ ����ǰ� �ֱ� ������, ������ ���� �۾��� �Ұ����մϴ�.
        // gameObject.SetActive(false); // ������Ʈ ��Ȱ��ȭ
        // �ڱ� �ڽſ� ���� ���� �۾��� �Ұ����մϴ�.
        // ���ο� ���� ������Ʈ�� �����ϴ� ���� �۾��� �����մϴ�.
    }
}
