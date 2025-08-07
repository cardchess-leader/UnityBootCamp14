using UnityEngine;
using TMPro;

public class TextMeshProSample : MonoBehaviour
{
    public TextMeshProUGUI text;

    private void Start()
    {
        // Rich Text example using tags
        text.text = "<size=150%>Hello, World!</size> <s>No hello world!</s>";
    }

    public void SetText(bool warning)
    {
        if (warning)
        {
            text.text = "<color=red>Warning: This is a warning message!</color>";
        }
        else
        {
            text.text = "<color=green>Info: This is an informational message.</color>";
        }
    }
}
