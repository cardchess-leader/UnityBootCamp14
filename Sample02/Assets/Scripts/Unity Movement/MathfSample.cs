using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
// �߿� Ŭ���� Mathf
// ����Ƽ���� ���� �������� �����Ǵ� ��ƿ��Ƽ Ŭ����
// ���� ���߿��� ���� �� �ִ� ���� ���꿡 ���� ���� �޼ҵ�� ����� �����մϴ�.

// ex)���� �޼ҵ� : static Ű����� ������ �ش� �޼ҵ�� Ŭ������.�޼ҵ��
// ���� ����� �����մϴ�. Mathf.Abs(-5)

public class MathfSample : MonoBehaviour
{
    // �⺻������ ���Ǵ� �޼ҵ�
    float abs = -5;
    float ceil = 4.1f;
    float floor = 4.6f;
    float round = 4.501f;
    float clamp = 4;
    float clamp01 = 1.2f;
    float pow = 3;
    float sqrt = 9;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(Mathf.Abs(abs)); // ����(absolute number)
        Debug.Log(Mathf.Ceil(ceil)); // �ø� (�Ҽ����� ������� ���� �ø� ó���մϴ�.)
        Debug.Log(Mathf.Floor(floor)); // ���� (�Ҽ����� ������� ���� ���� ó���մϴ�.)
        Debug.Log("4.5 Round is: " + Mathf.Round(round)); // �ݿø� (�Ҽ��� ù°�ڸ����� �ݿø��մϴ�.)
        Debug.Log(Mathf.Clamp(7, 0, clamp)); // ���� ���޹��� �� = 7, �ּ� = 0, �ִ� = 4, ��� -> 4.
                                         // ��, �ּ�, �ִ� ������ ���� �Է��մϴ�.

        Debug.Log(Mathf.Clamp01(clamp01)); // ���� ���޹��� �� = 5, �ּ� = 0, �ִ� = 1 --> ����� �ּڰ� �Ǵ� �ִ�
                                           // Same as Mathf.Clamp(value, 0, 1)
                                           // Clamp vs Clamp01
                                           // Clamp => ü��, ����, �ӵ� ���� �ɷ�ġ ���信���� ���� ����
                                           // Clamp01 -> ����(�ۼ�Ʈ), ������(0<=t<=1), ���� ��(���򿡼��� ����)
        Debug.Log("����: " + Mathf.Pow(pow, 2)); // ��, ���� ��(����)
        Debug.Log("������(pow, 0.5): " + Mathf.Pow(pow, 0.5f)); // ��, ���� ��(����) // ������ ������ ��� ���� ���� ���·� ���˴ϴ�.
        Debug.Log("������: " + Mathf.Sqrt(sqrt)); // ���� ���� �޾� �ش� ���� �������� ����մϴ�.

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
