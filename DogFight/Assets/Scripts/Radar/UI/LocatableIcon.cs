using UnityEngine;

/// <summary>
/// Concrete component for the icon being visible on the radar of a locatable
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class LocatableIcon : LocatableIconComponent
{
    protected CanvasGroup CanvasGroup { get; set; }

    protected virtual void Awake()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        SetVisible(false);
    }

    public override void SetVisible(bool visibility)
    {
        CanvasGroup.alpha = visibility ? 1.0f : 0.0f;
    }
}