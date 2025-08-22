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
    [SerializeField]
    private float visualRotationSpeed = 50f; // Speed of visual rotation for turning and pitching
    [SerializeField]
    private float rotationReturnConstant = 10f; // Speed of rotation return to 0 angle degree
    // Rigidbody component to apply physics forces
    Rigidbody rb;
    [SerializeField]
    Lift liftComponent;

    float timeSinceStart = 0f; // Timer to track how long the plane has been moving
    private bool canControl = false; // Flag to check if the player can control the plane

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component attached to the plane
        StartCoroutine(EnableControlCoroutine());
    }

    private void Update()
    {
        // This can be left empty or used for non-physics related updates
    }

    private void FixedUpdate()
    {
        // Apply thrust continuously
        float thrustPower = Mathf.Lerp(initThrustPower, maxThrustPower, timeSinceStart / accelerationTime);
        rb.AddForce(transform.forward * thrustPower * Time.fixedDeltaTime, ForceMode.Acceleration); // Apply forward force to the plane
        timeSinceStart += Time.fixedDeltaTime; // Increment the timer

        if (canControl)
        {
            HandlePlayerInput();
        }

        LimitSpeed();
        UpdateUI();
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

        // Apply visual rotation for the plane's model (Z-axis rotation)
        if (horizontalInput != 0)
        {
            float sidewaysAngularVelocity = Vector3.Dot(rb.angularVelocity, transform.up);
            rb.AddRelativeTorque(new Vector3(0, 0, -sidewaysAngularVelocity * visualRotationSpeed * Time.fixedDeltaTime), ForceMode.VelocityChange);
        } 
        else
        {
            // Return to original (z-axis) rotation to zero gradually
            float currentZRotation = transform.eulerAngles.z;
            float targetZRotation = 0f; // Target rotation angle
            //Debug.Log("Mathf.Abs(currentZRotation - targetZRotation): " + Mathf.Delta(currentZRotation - targetZRotation));
            float zRotationDelta = Mathf.MoveTowardsAngle(currentZRotation, targetZRotation, rotationReturnConstant * Mathf.Abs(Mathf.DeltaAngle(currentZRotation, targetZRotation)) / 4 * Time.fixedDeltaTime);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, zRotationDelta);
        }
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
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed; // Cap the speed at 100 m/s
        }
    }
}


// 1. The initial launch of the airplane is all fixed by scripts. No user control until the end of the initial launch.
// 2. There is a lift (upward force) when the airplane is below specific altitude. 
// 3. The airplane will descend (downward force) when the airplane is above specific altitude.