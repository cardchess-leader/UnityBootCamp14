using UnityEngine;
using UnityEngine.UI;

// 버튼의 onClick에 등록할 기능
// Removed unused Direction enum

public class SkeletonController : MonoBehaviour
{
    public Text scoreText;
    private float speed = 0.5f;
    public int score = 0;

    private void Start()
    {
        scoreText.text = "Score: " + score;
    }

    public void OnLButtonEnter()
    {
        // Move the skeleton to the left
        transform.Translate(Vector3.right * speed);
    }

    public void OnRButtonEnter()
    {
        // Move the skeleton to the right
        transform.Translate(Vector3.left * speed);
    }

    public void IncreaseScore()
    {
        // Increase the score by 1
        score++;
        SetScoreText();
    }

    public void DecreaseScore()
    {
        // Decrease the score by 10, but not below 0
        score = Mathf.Max(0, score - 10);
        SetScoreText();
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score;
    }

}
