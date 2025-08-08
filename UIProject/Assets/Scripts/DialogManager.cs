using System; // System 네임스페이스는 C#의 기본 클래스 라이브러리를 포함합니다.
using System.Collections; // 코루틴을 사용하기 위해 추가
using System.Collections.Generic; // Queue<T>를 사용하기 위해 추가
using UnityEngine; // UnityEngine 네임스페이스는 Unity의 기본 클래스와 기능을 포함합니다.
using TMPro; // TextMeshPro를 사용하기 위해 추가
using System.Text;
using UnityEngine.EventSystems; // StringBuilder를 사용하기 위해 추가

[Serializable]
public class Dialog
{
    public string character; // 캐릭터
    public string content; // 대화 텍스트

    // 오른쪽 버튼 > 빠른 작업 및 리팩토링 > 생성자 만들기
    public Dialog(string character, string content)
    {
        // this 키워드는 현재 클래스의 인스턴스를 참조합니다.        
        this.character = character;
        this.content = content;
    }
}
public class DialogManager : MonoBehaviour
{
    #region MonoSingleton
    //1) 자기 자신에 대한 인스턴스를 가진다. (전역)
    public static DialogManager Instance { get; private set; } // Property
    // Instance는 DialogManager 객체를 참조하는 정적 프로퍼티입니다.
    // Instance는 DialogManager의 유일한 인스턴스를 반환합니다.
    // Instance로는 수정은 안됩니다.

    private void Awake()
    {
        //2) 자기 자신을 인스턴스로 설정한다.
        if (Instance == null) // Instance가 null이면
        {
            Instance = this; // 자기 자신을 Instance로 설정
            DontDestroyOnLoad(gameObject); // 씬이 전환되어도 파괴되지 않도록 설정 (DDOL)
        }
        else
        {
            Destroy(gameObject); // 이미 Instance가 존재하면 현재 게임 오브젝트를 파괴
        }
    }
    #endregion

    #region Fields
    public TMP_Text text; 
    public TMP_Text characterName; 
    public GameObject panel;
    public float typeSpeed; // 타이핑 속도

    private Queue<Dialog> dialogQueue = new Queue<Dialog>(); // 대화 큐
    private Coroutine typing;
    private bool isTyping = false; // 타이핑 중인지 여부
    private Dialog currentDialog; // 현재 대화
    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 이벤트 시스템에 전달된 값이 존재하고, 그 값이 UI 요소 위에 있는지 확인합니다.
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                // UI 요소 위에 마우스가 있을 때는 아무 작업도 하지 않음
                return;
            }

            // 스페이스 눌러서 정상적으로 작업 중인 경우 (대화 매니저 있고, 대화가 진행 중인 경우)
            if (isTyping)
            {
                CompleteLine();
            } else
            {
                ShowNextLine();
            }
        }
    }

    public void StartDialog(IEnumerable<Dialog> dialogs)
    {
        dialogQueue.Clear(); // 대기열 초기화
        foreach (var dialog in dialogs) // 대화 내용을 대기열에 추가
        {
            dialogQueue.Enqueue(dialog);
        }
        panel.SetActive(true); // 패널 활성화
        ShowNextLine(); // 첫 번째 대화 표시
    }

    // 다음 작업을 위한 함수
    private void ShowNextLine()
    {
        // 대화할 내용이 더이상 없다면 종료
        if (dialogQueue.Count == 0)
        {
            DialogExit();
            return;
        }
        if (typing != null)
        {
            StopCoroutine(typing); // 현재 타이핑 중인 코루틴 중지
        }
        currentDialog = dialogQueue.Dequeue(); // 대기열에서 대화 꺼내기
        characterName.text = currentDialog.character; // 캐릭터 이름 설정
        typing = StartCoroutine(TypingDialog(currentDialog.content)); // 대화 내용 타이핑 시작
    }

    private void CompleteLine()
    {
        if (typing != null)
        {
            StopCoroutine(typing); // 타이핑 중인 코루틴 중지
        }
        text.text = currentDialog.content; // 현재 대화 내용 표시
        isTyping = false; // 타이핑 상태 변경
        typing = null; // 코루틴 참조 초기화
    }

    private void DialogExit() {
        CompleteLine();
        panel.SetActive(false);
    }

    private IEnumerator TypingDialog(string content)
    {
        isTyping = true; // 타이핑 시작
        StringBuilder builder = new StringBuilder(); // StringBuilder를 사용하여 문자열을 효율적으로 조작
        foreach (char c in content) // string(문자열)은 문자의 배열로 변환할 수 있습니다.
        {
            builder.Append(c); // 한 글자씩 추가
            text.text = builder.ToString(); // 현재까지의 문자열을 텍스트에 설정
            yield return new WaitForSeconds(typeSpeed); // 타이핑 속도만큼 대기
        }
        isTyping = false; // 타이핑 종료
        typing = null; // 코루틴 참조 초기화
    }
}





















// Queue<T>는 제네릭 컬렉션으로, 대기열의 형태로 데이터를 저장합니다.
// 데이터를 먼저 넣은 순서대로 꺼낼 수 있는 구조입니다.
// Queue<T>는 FIFO(First In First Out) 구조로, 가장 먼저 들어온 데이터가 가장 먼저 나갑니다.
// Queue<T>는 대기열, 버퍼, 작업 목록 등 다양한 상황에서 유용하게 사용됩니다.
// Queue<T>는 Enqueue() 메소드를 사용하여 데이터를 추가하고, Dequeue() 메소드를 사용하여 데이터를 꺼낼 수 있습니다.
// Queue<T>는 Count 프로퍼티를 사용하여 현재 대기열에 있는 데이터의 개수를 확인할 수 있습니다.
// Queue<T>는 Clear() 메소드를 사용하여 대기열을 비울 수 있습니다.
// Queue<T>는 Contains() 메소드를 사용하여 특정 데이터가 대기열에 있는지 확인할 수 있습니다.
// Queue<T>는 Peek() 메소드를 사용하여 대기열의 가장 앞에 있는 데이터를 확인할 수 있지만, 꺼내지는 않습니다.
// Queue<T>는 foreach 문을 사용하여 대기열의 모든 데이터를 순회할 수 있습니다.
// Queue<T>는 대기열의 데이터를 순서대로 처리할 때 유용합니다.
// Enqueue() 메소드는 데이터를 대기열의 끝에 추가합니다.
// Dequeue() 메소드는 대기열의 가장 앞에 있는 데이터를 꺼내고, 그 데이터를 대기열에서 제거합니다.(꺼낸 데이터를 반환합니다.)
// Peek() 메소드는 대기열의 가장 앞에 있는 데이터를 확인하지만, 꺼내지는 않습니다.
// Queue의 모든 Method 목록은 다음과 같습니다:
// Enqueue(T item), Dequeue(), Peek(), Clear(), Contains(T item), CopyTo(T[] array, int arrayIndex), ToArray(), TrimExcess(), GetEnumerator() 등입니다.
// Queue<string> message = new Queue<string>(); message.Enqueue("hello"); message.enqueue("world"); string firstMessage = message.Dequeue(); // "hello"가 firstMessage에 저장됩니다.


// StringBuilder는 문자열을 효율적으로 조작하기 위한 클래스입니다.
// 문자열을 연결, 삽입, 삭제 등의 작업을 빠르게 수행할 수 있습니다.
// StringBuilder는 문자열을 변경할 때마다 새로운 문자열을 생성하지 않고, 내부 버퍼를 사용하여 성능을 향상시킵니다.
// C#에서는 string은 불변(immutable) 객체이기 때문에, 문자열을 변경할 때마다 새로운 문자열 객체가 생성됩니다.
// StringBuilder는 가변(mutable) 객체로, 문자열을 변경할 때 내부 버퍼를 사용하여 성능을 향상시킵니다.
// 문자열의 경우 + 연산이 진행되면 새로운 문자열을 생성하는 구조
// StringBuilder의 기능: 1. 문자열 연결, 2. 문자열 삽입, 3. 문자열 삭제, 4. 문자열 치환, 5. 문자열 길이 확인 등

// 성능에 대한 비교:
// string에서의 + 연산 과정 vs StringBuilder의 Append() 메소드
// string은 불변(immutable) 객체이기 때문에, + 연산을 수행할 때마다 새로운 문자열 객체가 생성됩니다.
// StringBuilder는 가변(mutable) 객체로, 내부 버퍼를 사용하여 문자열을 효율적으로 조작합니다.
// StringBuilder는 문자열을 연결할 때 성능이 뛰어나며, 특히 반복적인 문자열 조작에 유리합니다.
// StringBuilder는 Append() 메소드를 사용하여 문자열을 연결할 수 있습니다.
// StringBuilder는 AppendLine() 메소드를 사용하여 문자열을 연결하고 줄바꿈을 추가할 수 있습니다.
// StringBuilder는 ToString() 메소드를 사용하여 최종 문자열을 반환할 수 있습니다.
// StringBuilder의 모든 Method 목록은 다음과 같습니다:
// Append(string value), AppendLine(string value), Insert(int index, string value), Remove(int startIndex, int length), Replace(string oldValue, string newValue), Clear(), ToString(), Length, Capacity 등입니다.
// StringBuilder의 사용 예시:
// StringBuilder sb = new StringBuilder(); sb.Append("Hello"); sb.Append(" World"); string result = sb.ToString(); // result는 "Hello World"가 됩니다.
// StringBuilder는 문자열을 효율적으로 조작할 때 유용합니다.
// StringBuilder는 문자열을 연결할 때 성능이 뛰어나며, 특히 반복적인 문자열 조작에 유리합니다.
// StringBuilder는 Append() 메소드를 사용하여 문자열을 연결할 수 있습니다.
// StringBuilder는 AppendLine() 메소드를 사용하여 문자열을 연결하고 줄바꿈을 추가할 수 있습니다.
// StringBuilder는 ToString() 메소드를 사용하여 최종 문자열을 반환할 수 있습니다.
// GC (가비지 컬렉션)와 관련하여:
// StringBuilder는 내부 버퍼를 사용하여 문자열을 효율적으로 조작합니다.
// StringBuilder는 문자열을 변경할 때마다 새로운 문자열 객체를 생성하지 않기 때문에, GC의 부담을 줄일 수 있습니다.
// StringBuilder는 문자열을 반복적으로 조작할 때 성능이 뛰어나며, GC의 부담을 줄이는 데 유리합니다.
// 실시간 조작 여부: Builder가 최적화 단계에서 더 유리합니다.



// 매니저 코드 (Manager.cs)
// 특정 기능이나 시스템을 중앙에서 관리하는 역할을 합니다.
// 대규모의 게임, UI, 데이터 공유 등에서 사용되는 핵심 역할
// 목적) 오브젝트나 시스템들을 한 곳에서 관리하는 용도
// 각 오브젝트가 개별적으로 로직을 처리하는 것이 아닌 매니저에 해당 기능을 위임합니다.
// 다른 씬에서도 동일한 매니저를 사용하여 일관된 기능을 제공합니다.

// 대표적인 예시) 1. 게임 매니저 (게임 시작, 종료, 정지, 씬 전환 등을 관리), 2. UI 매니저 (UI창에 대한 열고 닫음), 3. 오디오 매니저, 4. 데이터 매니저, 5. 씬 매니저 (씬 전환, 로딩, 언로딩 등, 유니티에서 제공), 6. 이벤트 매니저 (게임 내 이벤트 처리), 7. 플레이어 매니저 (플레이어 상태 관리), 8. 적 AI 매니저 (적의 행동 및 상태 관리) 등

// 매니저의 설계 방식(Singleton 패턴)
// 프로그램 전체에서 단 하나의 인스턴스만 존재하도록 보장하는 설계 방식

// 플레이어와 NPC가 충돌하면 대화 시작
// 마우스 클릭으로 NPC를 클릭하면 대화 시작