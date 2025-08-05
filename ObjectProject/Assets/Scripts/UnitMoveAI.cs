using System.Collections;
using UnityEngine;

public class UnitMoveAI : MonoBehaviour
{
    public float speed = 1.0f; // Speed of the unit
    public float detection = 5.0f; // Detection range for the unit
    Coroutine moveCoroutine;
    Transform playerPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player")?.transform; // Find the player object by tag
        if (playerPos != null)
        {
            moveCoroutine = StartCoroutine(MoveToPlayer());
        }
    }

    private void OnDisable()
    {
        StopCoroutine(moveCoroutine); // Stop the coroutine when the unit is disabled
    }

    IEnumerator MoveToPlayer()
    {
        while (playerPos != null)
        {
            float distance = Vector3.Distance(transform.position, playerPos.position);

            // �÷��̾ ������ ���� ���� ���� �ִ��� Ȯ��
            if (distance < detection)
            {
                Vector3 direction = (playerPos.position - transform.position).normalized; // ���� ���͸� ���
                transform.position = Vector3.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime); // �÷��̾� �������� �̵�
            }
            yield return null; // ���� �����ӱ��� ���
        }
    }
}
