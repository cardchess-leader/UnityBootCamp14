using UnityEngine;
using System.Collections;

public class CoroutineSample : MonoBehaviour
{
    // 적용할 타겟
    public GameObject target;
    public Color endColor;

    // 변화 시간
    public float duration = 5.0f;

    Coroutine coroutine; // 코루틴을 저장할 변수

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 타겟이 설정되어있다면
        if (target != null)
        {
            // 코루틴 시작
            coroutine = StartCoroutine(ChangeColor());
            // StopCoroutine(coroutine);
            // StopCoroutine("ChangeColor");
            // StopAllCoroutines(); // 모든 코루틴을 정지합니다.
            // StartCoroutine(메소드명());  // IEnumerator를 반환하는 메소드를 호출하여 코루틴을 시작합니다.
            // 코드 작성 과정에서 메소드가 결정되 안전함.
            // 메소드 호출은 컴파일 과정에서 확인되기에 찾아 실행하는 시간이 문자열보다 적게 듭니다.
            // StartCoroutine("ChangeColor");
            // StartCoroutine("메소드명"); 문자열을 통해 매개변수가 없는 코루틴을 호출할 수 있습니다.
            // 내부적으로 메소드의 이름을 문자열로 전달하여 해당 코루틴을 찾습니다.
            // 이 방법은 런타임에 문자열을 찾기 때문에 성능이 떨어질 수 있습니다.
            // 타입 체크를 하지 않아서 잘못된 이름을 사용하면 런타임 오류가 발생할 수 있습니다.
        }
        else
        {
            Debug.LogWarning("타겟이 등록되지 않았습니다.");
        }
    }

    IEnumerator ChangeColor()
    {
        var targetRenderer = target.GetComponent<Renderer>(); // 타겟으로부터 렌더러 컴포넌트에 대한 값을 얻어옵니다.

        if (targetRenderer == null) // 조사한 타겟의 렌더러가 없을 경우
        {
            Debug.LogError("Renderer component not found on the target GameObject.");
            yield break; // 렌더러가 없으면 코루틴을 종료합니다.
        }

        // 이 위치의 코드는 정상적으로 렌더러가 있는 경우에만 실행됩니다.
        float time  = 0.0f; // 시간 초기화

        // 타겟의 렌더러가 가진 머티리얼의 색상을 시작 색상으로 설정합니다.
        var start = targetRenderer.material.color; // 시작 색상
        var end = endColor; // 끝 색상

        // 반복 작업
        // 코루틴 내에서 반복문을 설계하면, yield에 의해 빠져나갔다가 다시 돌아와서 반복문을 실행하게 됩니다.
        while(true)
        {
            time += Time.deltaTime; // 시간 증가
            Debug.Log("현재 시간: " + time);
            var value = Mathf.PingPong(time, duration) / duration;
            // Mathf.PingPong(a, b)는 a와 b 사이에서 ping-pong 효과를 주는 함수입니다.

            targetRenderer.material.color = Color.Lerp(start, end, value);
            // 색상에 대한 부드러운 변경

            yield return new WaitForSeconds(1.0f); // 다음 프레임까지 대기
            Debug.Log("색상 변경 중: " + targetRenderer.material.color);
        }

    }

    // 코루틴 정지 기능

}

