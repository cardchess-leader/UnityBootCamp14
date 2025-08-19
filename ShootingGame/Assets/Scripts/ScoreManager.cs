using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ScoreManager : MonoBehaviour
{
    public GameObject stageClearPanel;
    public GameObject gameOverPanel;
    public Text scoreText;
    public Text bestText;
    public int stageClearScore = 100; // 스테이지 클리어 점수

    public static ScoreManager Instance = null;
    private int score = 0;
    private int bestScore = 0;
    private bool isGameRunning = true;

    // Add an event for stage clear
    public UnityEvent OnStageClear;

    void Awake()
    {
        // 싱글턴 패턴을 사용하여 ScoreManager의 인스턴스를 관리합니다.
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        // Fetch the best score from PlayerPrefs
        bestScore = PlayerPrefs.GetInt("BestScore", 0);

        // 초기 점수와 최고 점수를 업데이트합니다.
        UpdateScoreText();
        OnStageClear.AddListener(StageClear);
    }

    private void Update()
    {
        // 게임 오버 패널 또는 스테이지 클리어 패널이 활성화된 경우, R 키를 눌러 게임을 재시작할 수 있습니다.
        if (!isGameRunning)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

    private void StageClear()
    {
        if (!isGameRunning) return;
        stageClearPanel.SetActive(true); // 스테이지 클리어 패널 활성화
        isGameRunning = false; // 게임 상태를 비활성화로 변경
    }

    public void GameOver()
    {
        if (!isGameRunning) return;
        gameOverPanel.SetActive(true); // 게임 오버 패널 활성화
        isGameRunning = false; // 게임 상태를 비활성화로 변경
    }

    public void AddScore(int value)
    {
        score += value;
        if (bestScore < score)
        {
            bestScore = score;
            // Save best score to PlayerPrefs
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save();
            // PlayerPrefs를 사용한 이유는 심플한 int값 하나만 저장하고 불러오는 기능만 제공하면 되기 떄문입니다.
        }
        UpdateScoreText();
        if (score >= stageClearScore) //  100점 이상이면 스테이지 클리어
        {
            OnStageClear?.Invoke(); // 스테이지 클리어 이벤트 호출
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
        bestText.text = "Best: " + bestScore.ToString();
    }
}