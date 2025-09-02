using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    [Header("Required")]
    [SerializeField] private Image shadeImage;       // The dark overlay on top of your icon
    [SerializeField] private RectTransform spinner;  // Optional: a child image that rotates (can be null)

    [Header("Visuals")]
    [SerializeField] private float spinnerRPM = 30f; // Optional continuous rotation speed
    [SerializeField] private Color shadeColor = new Color(0, 0, 0, 0.63f); // ~alpha 0.63

    private float cooldownDuration;
    private float cooldownEndTime;
    private bool isCooling;

    void Awake()
    {
        if (shadeImage != null)
        {
            // Ensure proper setup
            shadeImage.type = Image.Type.Filled;
            shadeImage.fillMethod = Image.FillMethod.Radial360;
            shadeImage.fillOrigin = (int)Image.Origin360.Top;
            shadeImage.fillClockwise = false;
            shadeImage.color = shadeColor;
            shadeImage.fillAmount = 0f;   // 0 = no overlay; 1 = fully covered
            shadeImage.enabled = false;   // hidden when not cooling
        }
    }

    void Update()
    {
        if (!isCooling || shadeImage == null) return;

        float remaining = Mathf.Max(0f, cooldownEndTime - Time.time);
        float t = 1f - (remaining / cooldownDuration); // 0¡æ1 over cooldown
        shadeImage.fillAmount = 1f - t;                // pie shrinks clockwise

        // Optional: rotate a subtle spinner to give a ¡°moving alpha¡± feel
        if (spinner != null)
        {
            float degreesPerSecond = spinnerRPM * 360f / 60f;
            spinner.Rotate(0f, 0f, -degreesPerSecond * Time.deltaTime, Space.Self);
        }

        if (remaining <= 0f)
        {
            isCooling = false;
            shadeImage.enabled = false;
        }
    }

    /// <summary>
    /// Call this when the skill is used.
    /// </summary>
    public void StartCooldown(float seconds)
    {
        Debug.Log("Cooldown started for " + seconds + " seconds.");
        cooldownDuration = Mathf.Max(0.01f, seconds);
        cooldownEndTime = Time.time + cooldownDuration;
        isCooling = true;

        if (shadeImage != null)
        {
            shadeImage.enabled = true;
            shadeImage.fillAmount = 1f; // start fully shaded
        }
    }

//    // For quick testing: press Space to trigger cooldown
//#if UNITY_EDITOR
//    void LateUpdate()
//    {
//        if (Input.GetKeyDown(KeyCode.Space)) StartCooldown(5f);
//    }
//#endif
}
