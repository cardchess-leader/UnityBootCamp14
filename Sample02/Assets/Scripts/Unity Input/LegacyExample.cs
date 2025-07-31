using UnityEngine;
using UnityEngine.UI;

// Ű�� �Է��ϸ� �ؽ�Ʈ�� Ư�� �޽����� �������� �ϴ� �ڵ�
// Action Type: �׼� ������ �����մϴ�.
// Button: �������� ������ ���� ���θ� �����ϴ� �ܼ� ��ư �Է�

public class LegacyExample : MonoBehaviour
{
    public Text text;

    private void Start()
    {
        text = GetComponentInChildren<Text>();
        // GetComponentInChildren<T>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            text.text = "pata";
        }

        if (Input.GetKeyDown(KeyCode.Return)) // Enter
        {
            text.text = "pong";
        }

        if (Input.GetKeyDown(KeyCode.Escape)) // ESC
        {
            text.text = "";
        }

        foreach (KeyCode keycode in System.Enum.GetValues(typeof(KeyCode))) {
            if (Input.GetKeyDown(keycode))
            {
                text.text = keycode.ToString();
            }
        }
    }
}
