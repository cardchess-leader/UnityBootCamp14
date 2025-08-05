using UnityEngine;
using System.Collections;

public class IEnumeratorSample : MonoBehaviour
{
    class CustomCollection : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };
            foreach (int number in numbers)
            {
                yield return number;
            }   
        }
    }


    int[] numbers = { 1, 2, 3, 4, 5 };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IEnumerator enumerator = numbers.GetEnumerator();

        while (enumerator.MoveNext())
        {
            int number = (int)enumerator.Current;
            Debug.Log("Current number: " + number);
        }

        CustomCollection customCollection = new CustomCollection();
        // foreach는 순환 가능한 데이터를 자동으로 순회합니다.
        foreach (int number in customCollection)
        {
            Debug.Log("Number from CustomCollection: " + number);
        }
    
        foreach(var message in GetMessage())
        {
            Debug.Log("메시지: " + message);
        }
    }

    // yield는 C#에서 한 번에 하나씩 값을 넘기고, 메소드가 일시 정지되며 후속 값들을 지속적으로 반환하게 합니다.
    // yield는 yield return, yield break로 사용됩니다.

    // yield return은 값을 반환하고 메소드를 일시 정지합니다.
    // 호출자가 다음 값을 요청할 때 메소드는 이전 상태에서 계속 실행됩니다.

    // yield break는 순환 작업을 종료합니다. 더 이상 값을 반환하지 않습니다.
    // 컬렉션을 자동으로 순회하는 foreach와 자주 사용됩니다.

    // 장점: 값을 필요로 할 때까지 계산을 미루는 방식(메모리 효율이 좋음, 큰 데이터를 처리할 때 이점이 큽니다.)
    // -> 모든 데이터를 저장하는게 아닌 필요한 부분만 처리할 수 있게 되기 때문

    // IEnumerable: 반복 가능한 컬렉션을 나타내는 인터페이스입니다.
    // 이 기능을 구현한 클래스는 반복할 수 있는 객체가 되며 foreach 등에서 순차적인 탐색을 진행할 수 있게 됩니다.
    // 해당 인터페이스를 구현하기 위해서는 GetEnumerator() 메소드를 구현해야 합니다.

    // IEnumerator: 반복 가능한 컬렉션의 현재 위치를 나타내는 인터페이스입니다.
    // 이 인터페이스는 MoveNext() 메소드와 Current 속성을 포함합니다.
    // MoveNext() 메소드는 컬렉션의 다음 요소로 이동하고, Current 속성은 현재 요소를 반환합니다.
    // Current를 통해서 현재 값 확인
    // Reset()을 통해서 리셋 기능 처리

    static IEnumerable GetMessage() {
        Debug.Log("메소드 시작");
        yield return "야";
        Debug.Log("탈출 후 돌아옴 1");
        yield return "호";
        Debug.Log("탈출 후 돌아옴 2");
        yield break; // 순환 작업을 종료합니다.
        // Unreachable code
        Debug.Log("이 코드는 실행되지 않습니다.");
        yield return 3;
    }
}
