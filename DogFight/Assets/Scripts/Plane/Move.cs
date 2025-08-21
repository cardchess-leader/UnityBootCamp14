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
    float liftPower = 2000; // Lift power applied when the plane is moving fast enough to lift off
    [SerializeField]
    float accelerationTime = 5f; // Time in seconds to reach max thrust power
    [SerializeField]
    float liftOffTiming = 5f; // Lift off after this many seconds
    [SerializeField]
    float maxSpeed = 100f; // Maximum speed of the plane
    [SerializeField]
    GameObject forward;
    // Rigidbody component to apply physics forces
    Rigidbody rb;
    [SerializeField]
    Lift liftComponent;

    float timeSinceStart = 0f; // Timer to track how long the plane has been moving
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component attached to the plane
        StartCoroutine(LiftOffCoroutine());
    }

    private void Update()
    {
        float thrustPower = Mathf.Lerp(initThrustPower, maxThrustPower, timeSinceStart / accelerationTime);
        rb.AddForce(forward.transform.forward * thrustPower * Time.deltaTime, ForceMode.Acceleration); // Apply forward force to the plane
        timeSinceStart += Time.deltaTime; // Increment the timer
    }

    private void FixedUpdate()
    {
        LimitSpeed();
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

    IEnumerator LiftOffCoroutine()
    {
        yield return new WaitForSeconds(liftOffTiming); // Wait for the specified lift off timing
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