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
    public int stageClearScore = 100; // �������� Ŭ���� ����

    public static ScoreManager Instance = null;
    private int score = 0;
    private int bestScore = 0;
    private bool isGameRunning = true;

    // Add an event for stage clear
    public UnityEvent OnStageClear;

    void Awake()
    {
        // �̱��� ������ ����Ͽ� ScoreManager�� �ν��Ͻ��� �����մϴ�.
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        // Fetch the best score from PlayerPrefs
        bestScore = PlayerPrefs.GetInt("BestScore", 0);

        // �ʱ� ������ �ְ� ������ ������Ʈ�մϴ�.
        UpdateScoreText();
        OnStageClear.AddListener(StageClear);
    }

    private void Update()
    {
        // ���� ���� �г� �Ǵ� �������� Ŭ���� �г��� Ȱ��ȭ�� ���, R Ű�� ���� ������ ������� �� �ֽ��ϴ�.
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
        stageClearPanel.SetActive(true); // �������� Ŭ���� �г� Ȱ��ȭ
        isGameRunning = false; // ���� ���¸� ��Ȱ��ȭ�� ����
    }

    public void GameOver()
    {
        if (!isGameRunning) return;
        gameOverPanel.SetActive(true); // ���� ���� �г� Ȱ��ȭ
        isGameRunning = false; // ���� ���¸� ��Ȱ��ȭ�� ����
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
            // PlayerPrefs�� ����� ������ ������ int�� �ϳ��� �����ϰ� �ҷ����� ��ɸ� �����ϸ� �Ǳ� �����Դϴ�.
        }
        UpdateScoreText();
        if (score >= stageClearScore) //  100�� �̻��̸� �������� Ŭ����
        {
            OnStageClear?.Invoke(); // �������� Ŭ���� �̺�Ʈ ȣ��
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
        bestText.text = "Best: " + bestScore.ToString();
    }
}