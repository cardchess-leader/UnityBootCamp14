using System.Collections;
using UnityEngine;

// Detect mouse clicks on game objects with tag "Clickable"
public class ClickDetector : MonoBehaviour
{
    public ScoreController scoreController;
    private void Update()
    {
        // Check if the left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            // 2D ȯ�濡 �°� ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // 2D Raycast�� ����Ͽ� "Clickable" �±װ� �ִ� ������Ʈ ����
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Clickable"))
                {
                    scoreController.AddScore();
                    SpriteRenderer spriteRenderer = hit.collider.GetComponent<SpriteRenderer>();
                    if (spriteRenderer != null)
                    {
                        spriteRenderer.color = Color.red; // Ŭ���� ���������� ����
                        StartCoroutine(RestoreColor(spriteRenderer, Color.white));
                    }
                }
            }
        }
    }

    IEnumerator RestoreColor(SpriteRenderer spriteRenderer, Color originalColor)
    {
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColor; // ���� �������� ����
    }
}
