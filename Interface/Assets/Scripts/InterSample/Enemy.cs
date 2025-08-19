using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, ITakeDamage
{
    public int health = 100;

    public void TakeDamage(int damage)
    {
        if (damage <= 0)
        {
            return;
        }
        Debug.Log($"적이 {damage}만큼 데미지를 입었습니다.");
        if (damage <= 10)
        {
            StartCoroutine(DamageVisualEffect(Color.yellow));
        }
        else if (damage <= 20)
        {
            StartCoroutine(DamageVisualEffect(Color.magenta));
        }
        else
        {
            StartCoroutine(DamageVisualEffect(Color.red));
        }

        IEnumerator DamageVisualEffect(Color color)
        {
            // 1초동안 sprite render의 색상을 color로 변경
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                yield break; // SpriteRenderer가 없으면 코루틴 종료
            }
            Color originalColor = spriteRenderer.color;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(1f);
            // 1초 후에 원래 색상으로 되돌리기
            spriteRenderer.color = originalColor;
        }
    }
}
