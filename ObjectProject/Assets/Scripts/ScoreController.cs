using UnityEngine;
using TMPro;
public class ScoreController : MonoBehaviour
{
    // declare textmeshpro below
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    int score = 0;

    public void IncreaseScore()
    {
        score += 10;
        scoreText.text = "Score: " + score.ToString();
    }

    public void ShowGameOver()
    {
        gameOverText.gameObject.SetActive(true);
    }
}
