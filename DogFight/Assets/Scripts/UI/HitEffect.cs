using UnityEngine;
using UnityEngine.UI;

public class HitEffect : MonoBehaviour
{
    // Make this a singleton so that it can be accessed from other scripts
    public static HitEffect Instance { get; private set; }

    [Header("Assign the full-screen red Image")]
    [SerializeField] private Image overlay;

    [Header("Fade-out speed (alpha per second)")]
    [SerializeField] private float fadeSpeed = 2.5f;

    [Header("Map damage ratio (0..1) -> alpha")]
    [SerializeField]
    private AnimationCurve alphaByDamage =
        AnimationCurve.Linear(0f, 0f, 1f, 0.6f); // max ~60% red, tweak as you like

    private float currentAlpha;

    void Awake()
    {
        Debug.Log("HitEffect Awake");
        // Ensure that there is only one instance of HitEffect
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (overlay != null)
        {
            var c = overlay.color;
            c.a = 0f;
            overlay.color = c;
            overlay.raycastTarget = false;
        }
    }

    // Call this when the player takes damage.
    // damage: amount taken now; maxHealth: player's max (or pass the biggest expected hit).
    public void OnDamaged(float damage, float maxHealth)
    {
        Debug.Log("OnDamaged Called");
        if (overlay == null || maxHealth <= 0f) return;

        float normalizedDmg = damage / maxHealth;
        float ratio;
        if (normalizedDmg < 0.1)
        {
            ratio = 0.3f;
        } 
        else if (normalizedDmg < 0.25)
        {
            ratio = 0.6f;
        } else
        {
            ratio = 1f;
        }
        float target = alphaByDamage.Evaluate(ratio);               // 0..~0.6
        currentAlpha = Mathf.Clamp01(Mathf.Max(currentAlpha, target)); // stack hits nicely
        Apply();
    }

    void Update()
    {
        if (overlay == null || currentAlpha <= 0f) return;

        currentAlpha = Mathf.MoveTowards(currentAlpha, 0f, fadeSpeed * Time.deltaTime);
        Apply();
    }

    void Apply()
    {
        var c = overlay.color;
        c.a = currentAlpha;
        overlay.color = c;
    }
}