using System.Collections;
using UnityEngine;

// What this script does: 
// 1. Accelerate the plane forward when the game starts.
// 2. When the airplane speed reaches a certain threshold, it will start to lift off the ground.
// 3. And the plane's max speed is capped at a certain value.
// 4. After 5 seconds, the player will have control over the plane's movement. (with move keys)
public class Move : MonoBehaviour
{
    [SerializeField]
    float initThrustPower = 1000;
    [SerializeField]
    float maxThrustPower = 5000;
    [SerializeField]
    float accelerationTime = 5f; // Time in seconds to reach max thrust power
    [SerializeField]
    float liftOffTiming = 5f; // Lift off after this many seconds
    [SerializeField]
    float maxSpeed = 100f; // Maximum speed of the plane
    [SerializeField]
    private float rotationSpeed = 50f; // Speed of rotation for turning and pitching
    Rigidbody rb;
    [SerializeField]
    Lift liftComponent;
    float thrustPower; // Current thrust power

    [SerializeField]
    Material originalMat;
    [SerializeField]
    Material stealthMat;

    float timeSinceStart = 0f; // Timer to track how long the plane has been moving
    private bool canControl = false; // Flag to check if the player can control the plane
    Coroutine boostCoroutine;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component attached to the plane
        StartCoroutine(EnableControlCoroutine());
        StartCoroutine(InitThrust());
    }

    private void Update()
    {
        // If pressing space key, do something.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (boostCoroutine != null)
            {
                return;
            }
            boostCoroutine = StartCoroutine(BoostSpeed());
        }

        // E 키를 누르면 스텔스 모드 5초간 활성화
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(ActivateStealth());
        }
    }

    IEnumerator BoostSpeed()
    {
        Inventory.Instance.UseSkill(2);
        thrustPower *= 3; // Double the thrust power
        maxSpeed *= 2; // Double the max speed
        GetComponentInChildren<Propeller>()?.BoostRotationSpeed();
        yield return new WaitForSeconds(3f); // Boost lasts for 3 seconds
        thrustPower /= 3;
        maxSpeed /= 2;
        GetComponentInChildren<Propeller>()?.ResetRotationSpeed();
        boostCoroutine = null;
    }

    private void FixedUpdate()
    {
        // Apply thrust continuously
        rb.AddForce(transform.forward * thrustPower * Time.fixedDeltaTime, ForceMode.Acceleration); // Apply forward force to the plane
        timeSinceStart += Time.fixedDeltaTime; // Increment the timer

        if (canControl)
        {
            HandlePlayerInput();
        }

        LimitSpeed();
        UpdateUI();
    }

    IEnumerator InitThrust()
    {
        while(true)
        {
            if (thrustPower >= maxThrustPower)
            {
                thrustPower = maxThrustPower;
                yield break; // Exit the coroutine when max thrust power is reached
            }
            thrustPower = Mathf.Lerp(initThrustPower, maxThrustPower, timeSinceStart / accelerationTime);
            yield return null;
        }
    }

    private void HandlePlayerInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // A/D or Left/Right arrows
        float verticalInput = Input.GetAxis("Vertical"); // W/S or Up/Down arrows

        // Calculate rotation based on input
        // Vertical input controls pitch (tilting up/down around the X-axis)
        // Horizontal input controls yaw (turning left/right around the Y-axis)
        Vector3 rotationInput = new Vector3(verticalInput, horizontalInput, 0);

        // Apply torque to rotate the plane
        rb.AddRelativeTorque(rotationInput * rotationSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);
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

    IEnumerator EnableControlCoroutine()
    {
        yield return new WaitForSeconds(liftOffTiming); // Wait for the specified lift off timing

        // Enable player control & Start lifting
        canControl = true;
        liftComponent.enabled = true;
    }

    void LimitSpeed()
    {
        if (Speed > maxSpeed) // Assuming 100 m/s is the max speed
        {
            //rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed; // Cap the speed at 100 m/s
            rb.AddForce(-transform.forward * thrustPower * 1.1f * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }

    IEnumerator ActivateStealth()
    {
        Inventory.Instance.UseSkill(4);
        GetComponent<Player>().isStealthMode = true;
        yield return new WaitForSeconds(0.1f); // 약간의 딜레이
        // Switch to stealth material
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
        {
            if (stealthMat != null)
            {
                renderer.material = stealthMat;
            }
            Color originalColor = renderer.material.color;
            float elapsedTime = 0f;
            float duration = 1f; // 1초 동안 투명해짐
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(1f, 0.2f, elapsedTime / duration);
                renderer.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                yield return null;
            }
            renderer.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.2f);
        }
        yield return new WaitForSeconds(5f); // 5초간 스텔스 모드 유지
        // Switch back to original material
        foreach (var renderer in renderers)
        {
            if (originalMat != null)
            {
                renderer.material = originalMat;
            }
        }
        GetComponent<Player>().isStealthMode = false;
    }
}

// 1. The initial launch of the airplane is all fixed by scripts. No user control until the end of the initial launch.
// 2. There is a lift (upward force) when the airplane is below specific altitude. 
// 3. The airplane will descend (downward force) when the airplane is above specific altitude.
// 4. The airplane's model shows z-axis tilt rotation when moving left/right.
// 5. The view should also change based on the user mouse position. (The center of the screen is the center of the view)

// The x-axis sideways rotation is only applied to the model itself, not the parent rigidbody.