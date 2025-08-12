using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    int score = 0;
    int highScore = 0;
    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        ShowHighScore();
    }

    void ShowHighScore() {
        highScoreText.text = "High Score: " + highScore;
    }

    public void AddScore()
    {
        score++;
    }

    public void ShowScore()
    {
        scoreText.text = "Score: " + score;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
            ShowHighScore();
        }
    }
}
