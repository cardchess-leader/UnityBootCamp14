using UnityEngine;
using System.Collections;

public class CoroutineSample : MonoBehaviour
{
    // ������ Ÿ��
    public GameObject target;
    public Color endColor;

    // ��ȭ �ð�
    public float duration = 5.0f;

    Coroutine coroutine; // �ڷ�ƾ�� ������ ����

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Ÿ���� �����Ǿ��ִٸ�
        if (target != null)
        {
            // �ڷ�ƾ ����
            coroutine = StartCoroutine(ChangeColor());
            // StopCoroutine(coroutine);
            // StopCoroutine("ChangeColor");
            // StopAllCoroutines(); // ��� �ڷ�ƾ�� �����մϴ�.
            // StartCoroutine(�޼ҵ��());  // IEnumerator�� ��ȯ�ϴ� �޼ҵ带 ȣ���Ͽ� �ڷ�ƾ�� �����մϴ�.
            // �ڵ� �ۼ� �������� �޼ҵ尡 ������ ������.
            // �޼ҵ� ȣ���� ������ �������� Ȯ�εǱ⿡ ã�� �����ϴ� �ð��� ���ڿ����� ���� ��ϴ�.
            // StartCoroutine("ChangeColor");
            // StartCoroutine("�޼ҵ��"); ���ڿ��� ���� �Ű������� ���� �ڷ�ƾ�� ȣ���� �� �ֽ��ϴ�.
            // ���������� �޼ҵ��� �̸��� ���ڿ��� �����Ͽ� �ش� �ڷ�ƾ�� ã���ϴ�.
            // �� ����� ��Ÿ�ӿ� ���ڿ��� ã�� ������ ������ ������ �� �ֽ��ϴ�.
            // Ÿ�� üũ�� ���� �ʾƼ� �߸��� �̸��� ����ϸ� ��Ÿ�� ������ �߻��� �� �ֽ��ϴ�.
        }
        else
        {
            Debug.LogWarning("Ÿ���� ��ϵ��� �ʾҽ��ϴ�.");
        }
    }

    IEnumerator ChangeColor()
    {
        var targetRenderer = target.GetComponent<Renderer>(); // Ÿ�����κ��� ������ ������Ʈ�� ���� ���� ���ɴϴ�.

        if (targetRenderer == null) // ������ Ÿ���� �������� ���� ���
        {
            Debug.LogError("Renderer component not found on the target GameObject.");
            yield break; // �������� ������ �ڷ�ƾ�� �����մϴ�.
        }

        // �� ��ġ�� �ڵ�� ���������� �������� �ִ� ��쿡�� ����˴ϴ�.
        float time  = 0.0f; // �ð� �ʱ�ȭ

        // Ÿ���� �������� ���� ��Ƽ������ ������ ���� �������� �����մϴ�.
        var start = targetRenderer.material.color; // ���� ����
        var end = endColor; // �� ����

        // �ݺ� �۾�
        // �ڷ�ƾ ������ �ݺ����� �����ϸ�, yield�� ���� ���������ٰ� �ٽ� ���ƿͼ� �ݺ����� �����ϰ� �˴ϴ�.
        while(true)
        {
            time += Time.deltaTime; // �ð� ����
            Debug.Log("���� �ð�: " + time);
            var value = Mathf.PingPong(time, duration) / duration;
            // Mathf.PingPong(a, b)�� a�� b ���̿��� ping-pong ȿ���� �ִ� �Լ��Դϴ�.

            targetRenderer.material.color = Color.Lerp(start, end, value);
            // ���� ���� �ε巯�� ����

            yield return new WaitForSeconds(1.0f); // ���� �����ӱ��� ���
            Debug.Log("���� ���� ��: " + targetRenderer.material.color);
        }

    }

    // �ڷ�ƾ ���� ���

}

