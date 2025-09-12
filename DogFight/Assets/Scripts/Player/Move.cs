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

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component attached to the plane
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

    IEnumerator ActivateStealth()
    {
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