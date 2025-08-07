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
        // 씬 이동
        // 유의사항: 씬이 유니티 에디터에서 등록 되어 있어야 합니다.
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
        // 에디터에서 실행을 중지합니다.
         UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
