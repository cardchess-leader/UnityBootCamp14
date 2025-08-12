using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour
{
    public TMP_Text timeText;
    public ScoreController scoreController;
    public float remaingTime = 60f;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        remaingTime -= Time.deltaTime;
        timeText.text = "Time: " + Mathf.CeilToInt(remaingTime);
        if (remaingTime <= 0f && Time.timeScale != 0)
        {
            StopTime();
            scoreController.ShowScore();
        }
    }

    void StopTime()
    {
        Time.timeScale = 0f;
    }
}
