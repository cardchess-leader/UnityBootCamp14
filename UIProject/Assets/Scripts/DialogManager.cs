using System; // System ���ӽ����̽��� C#�� �⺻ Ŭ���� ���̺귯���� �����մϴ�.
using System.Collections; // �ڷ�ƾ�� ����ϱ� ���� �߰�
using System.Collections.Generic; // Queue<T>�� ����ϱ� ���� �߰�
using UnityEngine; // UnityEngine ���ӽ����̽��� Unity�� �⺻ Ŭ������ ����� �����մϴ�.
using TMPro; // TextMeshPro�� ����ϱ� ���� �߰�
using System.Text;
using UnityEngine.EventSystems; // StringBuilder�� ����ϱ� ���� �߰�

[Serializable]
public class Dialog
{
    public string character; // ĳ����
    public string content; // ��ȭ �ؽ�Ʈ

    // ������ ��ư > ���� �۾� �� �����丵 > ������ �����
    public Dialog(string character, string content)
    {
        // this Ű����� ���� Ŭ������ �ν��Ͻ��� �����մϴ�.        
        this.character = character;
        this.content = content;
    }
}
public class DialogManager : MonoBehaviour
{
    #region MonoSingleton
    //1) �ڱ� �ڽſ� ���� �ν��Ͻ��� ������. (����)
    public static DialogManager Instance { get; private set; } // Property
    // Instance�� DialogManager ��ü�� �����ϴ� ���� ������Ƽ�Դϴ�.
    // Instance�� DialogManager�� ������ �ν��Ͻ��� ��ȯ�մϴ�.
    // Instance�δ� ������ �ȵ˴ϴ�.

    private void Awake()
    {
        //2) �ڱ� �ڽ��� �ν��Ͻ��� �����Ѵ�.
        if (Instance == null) // Instance�� null�̸�
        {
            Instance = this; // �ڱ� �ڽ��� Instance�� ����
            DontDestroyOnLoad(gameObject); // ���� ��ȯ�Ǿ �ı����� �ʵ��� ���� (DDOL)
        }
        else
        {
            Destroy(gameObject); // �̹� Instance�� �����ϸ� ���� ���� ������Ʈ�� �ı�
        }
    }
    #endregion

    #region Fields
    public TMP_Text text; 
    public TMP_Text characterName; 
    public GameObject panel;
    public float typeSpeed; // Ÿ���� �ӵ�

    private Queue<Dialog> dialogQueue = new Queue<Dialog>(); // ��ȭ ť
    private Coroutine typing;
    private bool isTyping = false; // Ÿ���� ������ ����
    private Dialog currentDialog; // ���� ��ȭ
    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // �̺�Ʈ �ý��ۿ� ���޵� ���� �����ϰ�, �� ���� UI ��� ���� �ִ��� Ȯ���մϴ�.
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                // UI ��� ���� ���콺�� ���� ���� �ƹ� �۾��� ���� ����
                return;
            }

            // �����̽� ������ ���������� �۾� ���� ��� (��ȭ �Ŵ��� �ְ�, ��ȭ�� ���� ���� ���)
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
        dialogQueue.Clear(); // ��⿭ �ʱ�ȭ
        foreach (var dialog in dialogs) // ��ȭ ������ ��⿭�� �߰�
        {
            dialogQueue.Enqueue(dialog);
        }
        panel.SetActive(true); // �г� Ȱ��ȭ
        ShowNextLine(); // ù ��° ��ȭ ǥ��
    }

    // ���� �۾��� ���� �Լ�
    private void ShowNextLine()
    {
        // ��ȭ�� ������ ���̻� ���ٸ� ����
        if (dialogQueue.Count == 0)
        {
            DialogExit();
            return;
        }
        if (typing != null)
        {
            StopCoroutine(typing); // ���� Ÿ���� ���� �ڷ�ƾ ����
        }
        currentDialog = dialogQueue.Dequeue(); // ��⿭���� ��ȭ ������
        characterName.text = currentDialog.character; // ĳ���� �̸� ����
        typing = StartCoroutine(TypingDialog(currentDialog.content)); // ��ȭ ���� Ÿ���� ����
    }

    private void CompleteLine()
    {
        if (typing != null)
        {
            StopCoroutine(typing); // Ÿ���� ���� �ڷ�ƾ ����
        }
        text.text = currentDialog.content; // ���� ��ȭ ���� ǥ��
        isTyping = false; // Ÿ���� ���� ����
        typing = null; // �ڷ�ƾ ���� �ʱ�ȭ
    }

    private void DialogExit() {
        CompleteLine();
        panel.SetActive(false);
    }

    private IEnumerator TypingDialog(string content)
    {
        isTyping = true; // Ÿ���� ����
        StringBuilder builder = new StringBuilder(); // StringBuilder�� ����Ͽ� ���ڿ��� ȿ�������� ����
        foreach (char c in content) // string(���ڿ�)�� ������ �迭�� ��ȯ�� �� �ֽ��ϴ�.
        {
            builder.Append(c); // �� ���ھ� �߰�
            text.text = builder.ToString(); // ��������� ���ڿ��� �ؽ�Ʈ�� ����
            yield return new WaitForSeconds(typeSpeed); // Ÿ���� �ӵ���ŭ ���
        }
        isTyping = false; // Ÿ���� ����
        typing = null; // �ڷ�ƾ ���� �ʱ�ȭ
    }
}





















// Queue<T>�� ���׸� �÷�������, ��⿭�� ���·� �����͸� �����մϴ�.
// �����͸� ���� ���� ������� ���� �� �ִ� �����Դϴ�.
// Queue<T>�� FIFO(First In First Out) ������, ���� ���� ���� �����Ͱ� ���� ���� �����ϴ�.
// Queue<T>�� ��⿭, ����, �۾� ��� �� �پ��� ��Ȳ���� �����ϰ� ���˴ϴ�.
// Queue<T>�� Enqueue() �޼ҵ带 ����Ͽ� �����͸� �߰��ϰ�, Dequeue() �޼ҵ带 ����Ͽ� �����͸� ���� �� �ֽ��ϴ�.
// Queue<T>�� Count ������Ƽ�� ����Ͽ� ���� ��⿭�� �ִ� �������� ������ Ȯ���� �� �ֽ��ϴ�.
// Queue<T>�� Clear() �޼ҵ带 ����Ͽ� ��⿭�� ��� �� �ֽ��ϴ�.
// Queue<T>�� Contains() �޼ҵ带 ����Ͽ� Ư�� �����Ͱ� ��⿭�� �ִ��� Ȯ���� �� �ֽ��ϴ�.
// Queue<T>�� Peek() �޼ҵ带 ����Ͽ� ��⿭�� ���� �տ� �ִ� �����͸� Ȯ���� �� ������, �������� �ʽ��ϴ�.
// Queue<T>�� foreach ���� ����Ͽ� ��⿭�� ��� �����͸� ��ȸ�� �� �ֽ��ϴ�.
// Queue<T>�� ��⿭�� �����͸� ������� ó���� �� �����մϴ�.
// Enqueue() �޼ҵ�� �����͸� ��⿭�� ���� �߰��մϴ�.
// Dequeue() �޼ҵ�� ��⿭�� ���� �տ� �ִ� �����͸� ������, �� �����͸� ��⿭���� �����մϴ�.(���� �����͸� ��ȯ�մϴ�.)
// Peek() �޼ҵ�� ��⿭�� ���� �տ� �ִ� �����͸� Ȯ��������, �������� �ʽ��ϴ�.
// Queue�� ��� Method ����� ������ �����ϴ�:
// Enqueue(T item), Dequeue(), Peek(), Clear(), Contains(T item), CopyTo(T[] array, int arrayIndex), ToArray(), TrimExcess(), GetEnumerator() ���Դϴ�.
// Queue<string> message = new Queue<string>(); message.Enqueue("hello"); message.enqueue("world"); string firstMessage = message.Dequeue(); // "hello"�� firstMessage�� ����˴ϴ�.


// StringBuilder�� ���ڿ��� ȿ�������� �����ϱ� ���� Ŭ�����Դϴ�.
// ���ڿ��� ����, ����, ���� ���� �۾��� ������ ������ �� �ֽ��ϴ�.
// StringBuilder�� ���ڿ��� ������ ������ ���ο� ���ڿ��� �������� �ʰ�, ���� ���۸� ����Ͽ� ������ ����ŵ�ϴ�.
// C#������ string�� �Һ�(immutable) ��ü�̱� ������, ���ڿ��� ������ ������ ���ο� ���ڿ� ��ü�� �����˴ϴ�.
// StringBuilder�� ����(mutable) ��ü��, ���ڿ��� ������ �� ���� ���۸� ����Ͽ� ������ ����ŵ�ϴ�.
// ���ڿ��� ��� + ������ ����Ǹ� ���ο� ���ڿ��� �����ϴ� ����
// StringBuilder�� ���: 1. ���ڿ� ����, 2. ���ڿ� ����, 3. ���ڿ� ����, 4. ���ڿ� ġȯ, 5. ���ڿ� ���� Ȯ�� ��

// ���ɿ� ���� ��:
// string������ + ���� ���� vs StringBuilder�� Append() �޼ҵ�
// string�� �Һ�(immutable) ��ü�̱� ������, + ������ ������ ������ ���ο� ���ڿ� ��ü�� �����˴ϴ�.
// StringBuilder�� ����(mutable) ��ü��, ���� ���۸� ����Ͽ� ���ڿ��� ȿ�������� �����մϴ�.
// StringBuilder�� ���ڿ��� ������ �� ������ �پ��, Ư�� �ݺ����� ���ڿ� ���ۿ� �����մϴ�.
// StringBuilder�� Append() �޼ҵ带 ����Ͽ� ���ڿ��� ������ �� �ֽ��ϴ�.
// StringBuilder�� AppendLine() �޼ҵ带 ����Ͽ� ���ڿ��� �����ϰ� �ٹٲ��� �߰��� �� �ֽ��ϴ�.
// StringBuilder�� ToString() �޼ҵ带 ����Ͽ� ���� ���ڿ��� ��ȯ�� �� �ֽ��ϴ�.
// StringBuilder�� ��� Method ����� ������ �����ϴ�:
// Append(string value), AppendLine(string value), Insert(int index, string value), Remove(int startIndex, int length), Replace(string oldValue, string newValue), Clear(), ToString(), Length, Capacity ���Դϴ�.
// StringBuilder�� ��� ����:
// StringBuilder sb = new StringBuilder(); sb.Append("Hello"); sb.Append(" World"); string result = sb.ToString(); // result�� "Hello World"�� �˴ϴ�.
// StringBuilder�� ���ڿ��� ȿ�������� ������ �� �����մϴ�.
// StringBuilder�� ���ڿ��� ������ �� ������ �پ��, Ư�� �ݺ����� ���ڿ� ���ۿ� �����մϴ�.
// StringBuilder�� Append() �޼ҵ带 ����Ͽ� ���ڿ��� ������ �� �ֽ��ϴ�.
// StringBuilder�� AppendLine() �޼ҵ带 ����Ͽ� ���ڿ��� �����ϰ� �ٹٲ��� �߰��� �� �ֽ��ϴ�.
// StringBuilder�� ToString() �޼ҵ带 ����Ͽ� ���� ���ڿ��� ��ȯ�� �� �ֽ��ϴ�.
// GC (������ �÷���)�� �����Ͽ�:
// StringBuilder�� ���� ���۸� ����Ͽ� ���ڿ��� ȿ�������� �����մϴ�.
// StringBuilder�� ���ڿ��� ������ ������ ���ο� ���ڿ� ��ü�� �������� �ʱ� ������, GC�� �δ��� ���� �� �ֽ��ϴ�.
// StringBuilder�� ���ڿ��� �ݺ������� ������ �� ������ �پ��, GC�� �δ��� ���̴� �� �����մϴ�.
// �ǽð� ���� ����: Builder�� ����ȭ �ܰ迡�� �� �����մϴ�.



// �Ŵ��� �ڵ� (Manager.cs)
// Ư�� ����̳� �ý����� �߾ӿ��� �����ϴ� ������ �մϴ�.
// ��Ը��� ����, UI, ������ ���� ��� ���Ǵ� �ٽ� ����
// ����) ������Ʈ�� �ý��۵��� �� ������ �����ϴ� �뵵
// �� ������Ʈ�� ���������� ������ ó���ϴ� ���� �ƴ� �Ŵ����� �ش� ����� �����մϴ�.
// �ٸ� �������� ������ �Ŵ����� ����Ͽ� �ϰ��� ����� �����մϴ�.

// ��ǥ���� ����) 1. ���� �Ŵ��� (���� ����, ����, ����, �� ��ȯ ���� ����), 2. UI �Ŵ��� (UIâ�� ���� ���� ����), 3. ����� �Ŵ���, 4. ������ �Ŵ���, 5. �� �Ŵ��� (�� ��ȯ, �ε�, ��ε� ��, ����Ƽ���� ����), 6. �̺�Ʈ �Ŵ��� (���� �� �̺�Ʈ ó��), 7. �÷��̾� �Ŵ��� (�÷��̾� ���� ����), 8. �� AI �Ŵ��� (���� �ൿ �� ���� ����) ��

// �Ŵ����� ���� ���(Singleton ����)
// ���α׷� ��ü���� �� �ϳ��� �ν��Ͻ��� �����ϵ��� �����ϴ� ���� ���

// �÷��̾�� NPC�� �浹�ϸ� ��ȭ ����
// ���콺 Ŭ������ NPC�� Ŭ���ϸ� ��ȭ ����