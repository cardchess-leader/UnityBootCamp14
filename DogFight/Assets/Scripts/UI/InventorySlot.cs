using UnityEngine;
using TMPro;

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
}
