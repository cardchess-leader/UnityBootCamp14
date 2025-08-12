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
            // 2D 환경에 맞게 마우스 위치를 월드 좌표로 변환
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // 2D Raycast를 사용하여 "Clickable" 태그가 있는 오브젝트 감지
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Clickable"))
                {
                    scoreController.AddScore();
                    SpriteRenderer spriteRenderer = hit.collider.GetComponent<SpriteRenderer>();
                    if (spriteRenderer != null)
                    {
                        spriteRenderer.color = Color.red; // 클릭시 빨간색으로 변경
                        StartCoroutine(RestoreColor(spriteRenderer, Color.white));
                    }
                }
            }
        }
    }

    IEnumerator RestoreColor(SpriteRenderer spriteRenderer, Color originalColor)
    {
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColor; // 원래 색상으로 복원
    }
}
