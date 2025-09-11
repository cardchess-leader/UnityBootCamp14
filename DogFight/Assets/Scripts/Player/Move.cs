using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// What this script does: 
// 1. Accelerate the plane forward when the game starts.
// 2. When the airplane speed reaches a certain threshold, it will start to lift off the ground.
// 3. And the plane's max speed is capped at a certain value.
// 4. After 5 seconds, the player will have control over the plane's movement. (with move keys)
public class Move : MonoBehaviour
{
    Rigidbody rb;
    Material originalMat;
    [SerializeField]
    Material stealthMat;

    float timeSinceStart = 0f; // Timer to track how long the plane has been moving

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component attached to the plane
        // Set the original Material to originalMat for child called Plane from its skinned mesh renderer
        originalMat = GetComponentInChildren<Renderer>().material;
    }

    private void Update()
    {
        // E 키를 누르면 스텔스 모드 5초간 활성화
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(ActivateStealth());
        }
    }

    private void FixedUpdate()
    {
        // Apply thrust continuously
        timeSinceStart += Time.fixedDeltaTime; // Increment the timer
        UpdateUI();
    }

    private void UpdateUI()
    {
        UIController.Instance?.UpdateSpeedText(Speed);
        UIController.Instance?.UpdateAltitudeText(GameManager.Instance.GetAltitude(gameObject));
    }

    // include Speed getter property
    public float Speed
    {
        get { return rb.linearVelocity.magnitude; } // Return the magnitude of the velocity vector as speed
    }

    //    IEnumerator ActivateStealth()
    //    {
    //        Debug.Log("Activate Stealth");
    //        Inventory.Instance.UseSkill(4);
    //        GetComponent<Player>().isStealthMode = true;
    //        yield return new WaitForSeconds(0.1f); // 약간의 딜레이

    //        // Switch to stealth material and store original colors
    //        Renderer[] renderers = GetComponentsInChildren<Renderer>();
    //        Color[] originalColors = new Color[renderers.Length];
    //        Material[] originalMaterials = new Material[renderers.Length];

    //        for (int i = 0; i < renderers.Length; i++)
    //        {
    //            var renderer = renderers[i];

    //            // Skip TrailRenderer or renderers with materials that don't support _Color
    //            if (renderer is TrailRenderer || !renderer.material.HasProperty("_Color"))
    //            {
    //                originalColors[i] = Color.white; // Default color for unsupported materials
    //                originalMaterials[i] = renderer.material;
    //                continue;
    //            }

    //            // Store original color and material before changing
    //            originalColors[i] = renderer.material.color;
    //            originalMaterials[i] = renderer.material;

    //            // Switch to stealth material
    //            if (stealthMat != null)
    //            {
    //                renderer.material = stealthMat;
    //            }

    //            // Start transparency transition (fade to transparent)
    //            float elapsedTime = 0f;
    //            float duration = 1f; // 1초 동안 투명해짐
    //            Color stealthColor = renderer.material.color;

    //            while (elapsedTime < duration)
    //            {
    //                elapsedTime += Time.deltaTime;
    //                float alpha = Mathf.Lerp(1f, 0.2f, elapsedTime / duration);
    //                renderer.material.color = new Color(stealthColor.r, stealthColor.g, stealthColor.b, alpha);
    //                yield return null;
    //            }

    //            // Ensure final transparency
    //            renderer.material.color = new Color(stealthColor.r, stealthColor.g, stealthColor.b, 0.2f);
    //        }

    //        yield return new WaitForSeconds(5f); // 5초간 스텔스 모드 유지

    //        // Switch back to original material with fade-in transition
    //        for (int i = 0; i < renderers.Length; i++)
    //        {
    //            var renderer = renderers[i];

    //            // Skip TrailRenderer or renderers with materials that don't support _Color
    //            if (renderer is TrailRenderer || !originalMaterials[i].HasProperty("_Color"))
    //            {
    //                // Just restore the original material without color changes
    //                renderer.material = originalMaterials[i];
    //                continue;
    //            }

    //            // Switch back to original material
    //            renderer.material = originalMaterials[i];

    //            // Restore transparency transition (fade back to opaque)
    //            float elapsedTime = 0f;
    //            float duration = 1f; // 1초 동안 불투명해짐
    //            Color targetColor = originalColors[i];

    //            while (elapsedTime < duration)
    //            {
    //                elapsedTime += Time.deltaTime;
    //                float alpha = Mathf.Lerp(0.2f, 1f, elapsedTime / duration);
    //                renderer.material.color = new Color(targetColor.r, targetColor.g, targetColor.b, alpha);
    //                yield return null;
    //            }

    //            // Ensure final opacity with original color
    //            renderer.material.color = targetColor;
    //        }

    //        GetComponent<Player>().isStealthMode = false;
    //        Debug.Log("Deactivate Stealth");
    //    }
    //}

    IEnumerator ActivateStealth()
    {
        Debug.Log("Activate Stealth");
        Inventory.Instance.UseSkill(4);
        var player = GetComponent<Player>();
        player.isStealthMode = true;

        // Collect all renderers except TrailRenderer
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        var originalColors = new Dictionary<Renderer, Color>();
        var originalMaterials = new Dictionary<Renderer, Material>();

        foreach (var r in renderers)
        {
            if (r is TrailRenderer || !r.material.HasProperty("_Color"))
                continue;

            originalColors[r] = r.material.color;
            originalMaterials[r] = r.material;
        }

        // Fade out
        yield return Fade(renderers, 1f, 0.2f, 1f);

        // Hold for 5 seconds
        yield return new WaitForSeconds(5f);

        // Fade in
        yield return Fade(renderers, 0.2f, 1f, 1f, originalColors);

        // Restore original materials
        foreach (var kv in originalMaterials)
            kv.Key.material = kv.Value;

        player.isStealthMode = false;
        Debug.Log("Deactivate Stealth");
    }

    IEnumerator Fade(Renderer[] renderers, float from, float to, float duration,
                     Dictionary<Renderer, Color> restoreColors = null)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, elapsed / duration);

            foreach (var r in renderers)
            {
                if (r is TrailRenderer || !r.material.HasProperty("_Color")) continue;

                Color baseColor = restoreColors != null ? restoreColors[r] : r.material.color;
                r.material.color = new Color(baseColor.r, baseColor.g, baseColor.b, alpha);
            }

            yield return null;
        }
    }
}

// 1. The initial launch of the airplane is all fixed by scripts. No user control until the end of the initial launch.
// 2. There is a lift (upward force) when the airplane is below specific altitude. 
// 3. The airplane will descend (downward force) when the airplane is above specific altitude.
// 4. The airplane's model shows z-axis tilt rotation when moving left/right.
// 5. The view should also change based on the user mouse position. (The center of the screen is the center of the view)

// The x-axis sideways rotation is only applied to the model itself, not the parent rigidbody.