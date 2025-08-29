using UnityEngine;
using TMPro;

// This script displays time since start in mm:ss format on a TextMeshProUGUI text component

public class Timer : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI timerText;
    float elapsedTime = 0f;

    void Update()
    {
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
