using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    // Make this class a singleton
    public static UI Instance { get; private set; }

    public GameObject playerStatsPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Button newGameBtn;
    public Button continueGameBtn;

    public void NewGame()
    {
        playerStatsPanel.SetActive(true);
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene("EndScene");
    }

    public void DisableContinueBtn()
    {
        continueGameBtn.interactable = false;
    }

    public void OnCompleteSettings()
    {
        PlayerData playerData = DropDownController.Instance.GetSelectedPresetData();
        Data.Instance.SaveData(playerData);
    }
}
