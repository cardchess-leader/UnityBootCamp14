using UnityEngine;

public class View : MonoBehaviour
{
    [Header("Angles (degrees)")]
    public float maxYaw = 30f;    // left/right around Y
    public float maxPitch = 15f;  // up/down around X
    public float rollFromMouse = 0f; // optional bank around Z (set 0 to disable)

    [Header("Smoothing")]
    [Tooltip("Seconds to reach ~63% of target. Lower = snappier")]
    public float smoothTime = 0.08f;

    [Header("Input shaping")]
    [Tooltip("Ignore tiny mouse offsets (0..1 in screen-normalized units)")]
    public float deadzone = 0.03f;
    [Tooltip("Clamp radial extent from center (0..1). 1 = screen edge")]
    public float clampRadius = 1f;

    Quaternion baseLocalRot;
    Vector3 vel; // SmoothDampAngle velocities (x=pitch, y=yaw, z=roll)

    void OnEnable() => baseLocalRot = transform.localRotation;

    void LateUpdate()
    {
        // 1) Mouse normalized to [-1..1] from screen center
        Vector2 center = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        Vector2 half = center;
        Vector2 mPos = (Vector2)Input.mousePosition;
        Vector2 n = new Vector2(
            half.x > 0 ? (mPos.x - center.x) / half.x : 0f,
            half.y > 0 ? (mPos.y - center.y) / half.y : 0f
        );

        // radial deadzone
        float r = n.magnitude;
        if (r < deadzone) n = Vector2.zero;
        else n *= (r - deadzone) / Mathf.Max(1e-5f, (1f - deadzone));

        // clamp radius
        n = Vector2.ClampMagnitude(n, Mathf.Clamp01(clampRadius));

        // 2) Target local Euler offsets (deg)
        float targetYaw = n.x * maxYaw;      // right positive
        float targetPitch = -n.y * maxPitch;    // invert so up mouse -> look up
        float targetRoll = -n.x * rollFromMouse;

        // 3) Current local delta from base
        Vector3 delta = (Quaternion.Inverse(baseLocalRot) * transform.localRotation).eulerAngles;
        delta.x = Mathf.DeltaAngle(0f, delta.x);
        delta.y = Mathf.DeltaAngle(0f, delta.y);
        delta.z = Mathf.DeltaAngle(0f, delta.z);

        // 4) Smooth toward targets
        float x = Mathf.SmoothDampAngle(delta.x, targetPitch, ref vel.x, smoothTime);
        float y = Mathf.SmoothDampAngle(delta.y, targetYaw, ref vel.y, smoothTime);
        float z = Mathf.SmoothDampAngle(delta.z, targetRoll, ref vel.z, smoothTime);

        // 5) Apply relative to starting orientation
        transform.localRotation = baseLocalRot * Quaternion.Euler(x, y, z);
    }
}
