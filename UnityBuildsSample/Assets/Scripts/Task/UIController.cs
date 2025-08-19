using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    // add textmeshpro button as public field
    public Button gameStartBtn, gameEndBtn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // register button click events
        gameStartBtn.GetComponent<Button>().onClick.AddListener(OnGameStartButtonClick);
        gameEndBtn.GetComponent<Button>().onClick.AddListener(OnGameEndButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGameStartButtonClick()
    {
        // move to game scene
        SceneManager.LoadScene("GameScene");
    }

    void OnGameEndButtonClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // stop the game in editor
#else
        Application.Quit(); // quit the application in build
#endif
    }
}
