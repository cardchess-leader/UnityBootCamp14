using UnityEngine;
using UnityEngine.UI;

public class UpgradeRow : MonoBehaviour
{
    Button upgradeBtn;
    ProgressBar progressBar;
    public UpgradeType upgradeType;
    [SerializeField] UpgradeUI upgradeUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Find "Upgrade Button" in children and assign it to upgradeBtn
        upgradeBtn = transform.Find("Upgrade Button").GetComponent<Button>();
        progressBar = transform.Find("Progress Bar").GetComponent<ProgressBar>();
        // Add listener to upgradeBtn
        upgradeBtn.onClick.AddListener(OnUpgradeButtonClick);
    }

    void OnUpgradeButtonClick()
    {
        progressBar.Value++;
        upgradeUI.OnUpgrade(upgradeType);
    }

    public void SetProgressValue(int value, bool btnActive)
    {
        progressBar.Value = value;
        if (progressBar.Value == progressBar.maxValue || !btnActive)
        {
            upgradeBtn.interactable = false;
        }
        else
        {
            upgradeBtn.interactable = true;
        }
    }
}
