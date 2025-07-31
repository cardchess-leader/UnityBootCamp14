using UnityEngine;
using UnityEngine.UI;

// 키를 입력하면 텍스트에 특정 메시지가 나오도록 하는 코드
// Action Type: 액션 유형을 선택합니다.
// Button: 눌렀는지 땠는지 등의 여부만 감지하는 단순 버튼 입력

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
