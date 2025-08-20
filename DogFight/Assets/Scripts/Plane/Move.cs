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
    float liftOffSpeed = 50f; // Speed at which the plane will lift off
    [SerializeField]
    float maxSpeed = 100f; // Maximum speed of the plane
    // Rigidbody component to apply physics forces
    Rigidbody rb;
    float timeSinceStart = 0f; // Timer to track how long the plane has been moving
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component attached to the plane
    }

    private void Update()
    {
        // thrust power is calculated based on the time since the game started
        float thrustPower = Mathf.Lerp(initThrustPower, maxThrustPower, timeSinceStart / accelerationTime);
        rb.AddForce(transform.forward * thrustPower * Time.deltaTime, ForceMode.Acceleration); // Apply forward force to the plane
        timeSinceStart += Time.deltaTime; // Increment the timer
    }

    // Invoke the UIController to update the speed text.
    private void FixedUpdate()
    {
        // Calculate the speed of the plane
        float speed = rb.linearVelocity.magnitude;
        // Update the UI with the current speed
        UIController.Instance.UpdateSpeedText(speed);
        
        // Limit the maximum speed of the plane
        if (speed > maxSpeed) // Assuming 100 m/s is the max speed
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed; // Cap the speed at 100 m/s
        }
    }

}
