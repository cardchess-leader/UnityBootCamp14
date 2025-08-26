using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField]
    TMP_Text speedText;
    [SerializeField]
    TMP_Text altitudeText;
    [SerializeField]
    TMP_Text hpText;
    [SerializeField]
    TMP_Text ammoText;
    [SerializeField]
    TMP_Text expText;
    [SerializeField]
    TMP_Text levelText;
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
        UpdateAltitudeText();
    }
    public void UpdateSpeedText(float speed = 0f)
    {
        speedText.text = $"Speed: {speed:F2} m/s"; // Update the speed text with the current speed
    }
    public void UpdateAltitudeText(float altitude = 0f)
    {
        altitudeText.text = $"Altitude: {altitude:F2} m"; // Update the speed text with the current speed
    }
    public void UpdateHpText(int hp, int maxHp)
    {
        hpText.text = $"HP: {hp} / {maxHp}";
    }
    public void UpdateAmmoText(int ammo, int maxAmmo)
    {
        ammoText.text = $"Ammo: {ammo} / {maxAmmo}";
    }
    public void ShowReloadingText()
    {
        ammoText.text = "Reloading Ammo...";
    }

    public void UpdateLevelText(int level, int exp, int nextLevelExp)
    {
        expText.text = $"EXP: {exp} / {nextLevelExp}";
        levelText.text = $"LEVEL: {level}";
    }
}
