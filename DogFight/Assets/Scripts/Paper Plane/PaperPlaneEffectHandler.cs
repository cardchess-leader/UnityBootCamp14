using UnityEngine;

public class PaperPlaneEffectHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem speedlinesEffect;
    [SerializeField] private float speedlinesEmissionBoostRate = 150f;

    private BasePlaneController _planeController;

    private void Awake()
    {
        _planeController = GetComponent<BasePlaneController>();
    }

    private void OnEnable()
    {
        _planeController.OnCrash += HandleCrash;
    }

    private void OnDisable()
    {
        _planeController.OnCrash -= HandleCrash;
    }

    private void HandleCrash()
    {
        var module = speedlinesEffect.emission;
        module.rateOverTime = 0f;
    }

    void Update()
    {
        if (!speedlinesEffect) return; // guard in case the PS got destroyed/replaced

        var emission = speedlinesEffect.emission; // get a fresh module every time
        float target = _planeController.IsBoosting ? speedlinesEmissionBoostRate : 0f;

        // Smoothly approach the target using the *multiplier* (a plain float)
        emission.rateOverTimeMultiplier = Mathf.Lerp(
            emission.rateOverTimeMultiplier,
            target,
            Time.deltaTime * 10f
        );
    }
}