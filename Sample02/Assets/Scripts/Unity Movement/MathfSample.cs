using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
// 중요 클래스 Mathf
// 유니티에서 수학 관련으로 제공되는 유틸리티 클래스
// 게임 개발에서 사용될 수 있는 수학 연산에 대한 정적 메소드와 상수를 제공합니다.

// ex)정적 메소드 : static 키워드로 구성된 해당 메소드는 클래스명.메소드명
// 으로 사용이 가능합니다. Mathf.Abs(-5)

public class MathfSample : MonoBehaviour
{
    // 기본적으로 사용되는 메소드
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
        Debug.Log(Mathf.Abs(abs)); // 절댓값(absolute number)
        Debug.Log(Mathf.Ceil(ceil)); // 올림 (소수점과 상관없이 값을 올림 처리합니다.)
        Debug.Log(Mathf.Floor(floor)); // 내림 (소수점과 상관없이 값을 내림 처리합니다.)
        Debug.Log("4.5 Round is: " + Mathf.Round(round)); // 반올림 (소숫점 첫째자리에서 반올림합니다.)
        Debug.Log(Mathf.Clamp(7, 0, clamp)); // 현재 전달받은 값 = 7, 최소 = 0, 최대 = 4, 결과 -> 4.
                                         // 값, 최소, 최대 순으로 값을 입력합니다.

        Debug.Log(Mathf.Clamp01(clamp01)); // 현재 전달받은 값 = 5, 최소 = 0, 최대 = 1 --> 벗어나면 최솟값 또는 최댓값
                                           // Same as Mathf.Clamp(value, 0, 1)
                                           // Clamp vs Clamp01
                                           // Clamp => 체력, 점수, 속도 등의 능력치 개념에서의 범위 제한
                                           // Clamp01 -> 비율(퍼센트), 보간값(0<=t<=1), 알파 값(색깔에서의 투명도)
        Debug.Log("제곱: " + Mathf.Pow(pow, 2)); // 값, 제곱 수(지수)
        Debug.Log("제곱근(pow, 0.5): " + Mathf.Pow(pow, 0.5f)); // 값, 제곱 수(지수) // 지수가 음수일 경우 값은 역수 형태로 계산됩니다.
        Debug.Log("제곱근: " + Mathf.Sqrt(sqrt)); // 값을 전달 받아 해당 값의 제곱근을 계산합니다.

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
