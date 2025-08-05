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

            // 플레이어가 유닛의 감지 범위 내에 있는지 확인
            if (distance < detection)
            {
                Vector3 direction = (playerPos.position - transform.position).normalized; // 방향 벡터를 계산
                transform.position = Vector3.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime); // 플레이어 방향으로 이동
            }
            yield return null; // 다음 프레임까지 대기
        }
    }
}
