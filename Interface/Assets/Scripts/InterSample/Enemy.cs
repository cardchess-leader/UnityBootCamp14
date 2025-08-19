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
        Debug.Log($"���� {damage}��ŭ �������� �Ծ����ϴ�.");
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
            // 1�ʵ��� sprite render�� ������ color�� ����
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                yield break; // SpriteRenderer�� ������ �ڷ�ƾ ����
            }
            Color originalColor = spriteRenderer.color;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(1f);
            // 1�� �Ŀ� ���� �������� �ǵ�����
            spriteRenderer.color = originalColor;
        }
    }
}
