using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;

    public GameObject ruleUI;

    private void Start()
    {
        button1.onClick.AddListener(GameStart);
        button2.onClick.AddListener(RuleView);
        button3.onClick.AddListener(GameExit);
    }

    private void GameStart()
    {
        // �� �̵�
        // ���ǻ���: ���� ����Ƽ �����Ϳ��� ��� �Ǿ� �־�� �մϴ�.
        SceneManager.LoadScene("SampleScene");
    }

    private void RuleView()
    {
        ruleUI.SetActive(!ruleUI.activeSelf);
    }

    private void GameExit()
    {
#if UNITY_EDITOR
        //EditorApplication.Exit(0);
        //Application.Quit();
        // �����Ϳ��� ������ �����մϴ�.
         UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
