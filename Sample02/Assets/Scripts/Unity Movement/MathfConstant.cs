using UnityEngine;

public class MathfConstant : MonoBehaviour
{
    void Start()
    {
        Debug.Log(Mathf.PI); // 3.14159.....
        Debug.Log(Mathf.Infinity); // ���Ѵ�
        // ������ ���꿡 ���ؼ� ǥ���� �� �ִ� �ִ��� ���� �Ѵ� ����� �ڵ����� ó���Ǵ� ��
        // ���������� Infinity�� �ۼ��� ��������� ���Ѵ븦 ǥ���ϱ⵵ �մϴ�.
        // 1) Pow(0, -2) = 1 / 0 (Infinity)
        // 2) float ������ ǥ���� �� ���� ū ���� �����ϴ� ���, ���� ����� ���
        // float�� �ִ� �� == float.MaxValue


        Debug.Log(Mathf.NegativeInfinity); // ���� ���Ѵ�
        //1) ������ ���� ���� ������ �����÷ο� �Ǵ� ���
        //2) ���������� NegativeInfinity�� ��õǴ� ���

        Debug.Log(Mathf.Sqrt(-1)); // NaN(Not a Number) : ���������� ���ǵ��� ���� ��� ����� ��� ������ ��

        // ���� ���Ѵ�� � ���ڵ� �� �� ���� ���� ������ ����Ű�� ����
        // NaN�� ���Ͽ�
        // 1. �� ���� NaN�� ��� �� ���� ���� �񱳴� �Ұ����մϴ�. (������ üũ�ϸ� false)
        // float.IsNaN(��)�� ���� ���� NaN���� Ȯ���� �����ϴ�.
        // position�� ���� NaN�̴�? ������Ʈ�� ������ ������ϴ�.
        // Rigidbody���� ����ϴ� �� �� velocity�� NaN�̶��? ���� ���� �ۿ뿡 ���� ����
        //2. infinity - infinity = NaN
        // 0 / 0 �� ���� ���������� ���� �ƿ� �ȵǴ� ��
        // ������ ���� ��Ʈ ��� (����� ���Ҽ� ���� ������ ����ڰ� ���� ������ ������� �����Ѵ�.)
        // -> ����Ƽ�� C#���� ����� ���� �������� ������ ���� �ʽ��ϴ�.
        // ��� -> ���� ������ Sqrt(-1)
        // C# System.Numerics.Complex ����� ���� ����� ���� ����� �����մϴ�.
        // using System.Numerics;
        // Complex c = Complex.Sqrt(-1)
        // ����) ����Ƽ ���� ���ؿ� ���� ����� ���ѵǴ� ��찡 �ֽ��ϴ�. (ex WebGL)
        Vector3 pos;
        if (float.IsNaN(transform.position.x) )
        {
            pos = transform.position;
            pos.x = 0;
            transform.position = pos;
        }

        pos = transform.position;
        //pos.x = Mathf.Infinity;

        transform.position = pos;

        Debug.Log("-1^0.5 is: " + Mathf.Sqrt(-1));

        // �� �̿��� �Լ� / ��
        // 1. Mathf.Deg2Rad() // Radian: �������� ���� ���̸� ���� ȣ�� ���� �߽ɰ� = 1 ����(57��, �� 60��)
        // 2. Mathf.Rad2Rad() // ����Ƽ���� �������ִ� �ﰢ�Լ� ��꿡�� ���� ��� ������ �䱸��. 
        // 3. Mathf.Epsilon : float������ 0�� �ƴ� ���� ���� ��� ��(���� 0�� ����� �� -> �̼��� ���� �ٷ� �� �����
        // float���� 0f���� Epsilon���� 0�� üũ�ϸ� �����ϰ� ���˴ϴ�. �Ǵ� 0���� ������ ��Ȳ�� �����մϴ�.

        // ����Ƽ������ �� �� ����� ���� ���� -> ��, �Ǵ� �� -> ���������� ��ȯ�� ó���մϴ�.

        // ���� ���Ǵ� degree�� ������ ��
        // 0,   90, 180, 360
        // 0, PI/2,  PI, 2PI

        // Degree�� ���Ǵ� ���: transform.Rotate(), transform.eulerAngles -> Ʈ������������ (x, y, z) ���� ǥ��(��), Quaternion.AngleAxis(45f, Vector3.up) // 45����ŭ ȸ��
        // Quaternion.Angle(A, B) -> �� ȸ�� ���� ���̸� ��Ÿ���� ����(in degrees)
        // ����Ƽ �ν����Ϳ��� �������� ȸ�� �Է� (in degrees)
        // ���: ������ ���� ����, ���ӵ�, �̺� ���� �۾��� ������ �� ����� �� �����ϰ� ����ǰ� ���� ����Ƽ ��� ���Ǵ� �ﰢ �Լ��� ���� ���Ŀ��� ���˴ϴ�. 
        // ���: degree : �������� ȸ���� ���� ǥ�� (�Է�, �������� ��)
        //       radian : �ﰢ �Լ� ��꿡 ���Ǵ� ���� ǥ��
    }
}
