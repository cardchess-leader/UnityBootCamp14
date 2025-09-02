using UnityEngine;
using TMPro;
using System.Collections;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private GameObject itemIcon;
    [SerializeField] private GameObject highlightBorder;
    private bool isHighlighted = false;
    private bool hasItem = false;
    [SerializeField] int slotIndex = 1;

    private void Start()
    {
        // Find textmeshpro component in children
        var indexText = GetComponentInChildren<TextMeshProUGUI>();
        if (indexText != null)
        {
            indexText.text = slotIndex.ToString();
        }
    }

    public void Highlight(bool highlight)
    {
        if (highlightBorder != null)
        {
            highlightBorder.SetActive(highlight);
        }
    }

    // Implement a method that shows shade overlay effect (clockwise rotating alpha blending) when the skill is under cooldown
    public void ShowCooldownEffect(float cooldownTime)
    {
        // Highlight border gives alpha blending effect (clockwise rotating)
        if (highlightBorder != null)
        {
            // Start coroutine to show cooldown effect
            StartCoroutine(CooldownEffectCoroutine(cooldownTime));
        }
    }

    IEnumerator CooldownEffectCoroutine(float cooldownTime)
    {
        float elapsed = 0f;
        var image = highlightBorder.GetComponent<UnityEngine.UI.Image>();
        if (image == null)
        {
            yield break;
        }
        image.fillAmount = 1f; // Start with full overlay
        while (elapsed < cooldownTime)
        {
            elapsed += Time.deltaTime;
            image.fillAmount = 1f - (elapsed / cooldownTime);
            yield return null;
        }
        image.fillAmount = 0f; // End with no overlay
    }
}
