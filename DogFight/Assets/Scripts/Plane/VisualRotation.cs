using UnityEngine;
 
public class VisualRotation : MonoBehaviour
{
    [Header("Tilt settings")]
    [Tooltip("Max absolute Z tilt in degrees (e.g., 20)")]
    public float maxTiltDeg = 20f;

    [Tooltip("How fast we lean toward target (deg/sec)")]
    public float tiltSpeed = 240f;

    [Tooltip("How fast we return to 0 when no input (deg/sec)")]
    public float returnSpeed = 300f;

    void Update()
    {
        // A = left (counter-clockwise Z), D = right (clockwise Z)
        float input = 0f;
        if (Input.GetKey(KeyCode.A)) input -= 1f;
        if (Input.GetKey(KeyCode.D)) input += 1f;

        // Desired Z angle in degrees: left = +max, right = -max (counter-clockwise is + in Unity)
        float targetZ = -input * maxTiltDeg;

        // Current local Z (0..360) -> use angle-safe helpers
        float currentZ = transform.localEulerAngles.z;

        // Choose speed: lean fast while pressed, return speed when released
        float speed = (Mathf.Approximately(input, 0f) ? returnSpeed : tiltSpeed);

        // Move the Z angle smoothly toward target
        float newZ = Mathf.MoveTowardsAngle(currentZ, targetZ, speed * Time.deltaTime);

        // Apply only the Z change, preserving X/Y local euler (usually 0)
        var e = transform.localEulerAngles;
        e.z = newZ;
        transform.localEulerAngles = e;
    }
}
