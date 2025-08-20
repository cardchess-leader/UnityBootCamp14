using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField]
    TMP_Text speedText;
    // Make this singleton so that it can be accessed from other scripts
    public static UIController Instance { get; private set; }
    private void Awake()
    {
        // Ensure that there is only one instance of UIController
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateSpeedText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateSpeedText(float speed = 0f)
    {
        speedText.text = $"Speed: {speed:F2} m/s"; // Update the speed text with the current speed
    }
}
