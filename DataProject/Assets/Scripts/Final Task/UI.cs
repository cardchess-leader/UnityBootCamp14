using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    // Make this class a singleton
    public static UI Instance { get; private set; }

    public GameObject playerStatsPanel;
    public Image classImage;

    public Text strText;
    public Text dexText;
    public Text intlText;
    public Text lukText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Instance = null;
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
        Data.Instance.SaveData(DropDownController.Instance.GetSelectedPreset.playerData);
    }

    public void SetClassImage(Sprite sprite)
    {
        // Assuming you have an Image component to set the class image
        if (classImage != null)
        {
            classImage.sprite = sprite;
        }
    }

    public void SetStatsText(PlayerData selectedStat)
    {
        strText.text = "STR: " + selectedStat.str;
        dexText.text = "DEX: " + selectedStat.dex;
        intlText.text = "INT: " + selectedStat.intl;
        lukText.text = "LUK: " + selectedStat.luk;
    }

    public void OnResetBtnClicked()
    {
        Data.Instance.ClearData();
    }

    public void OnEndGameBtnClicked()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
