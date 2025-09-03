using UnityEngine;

public class TargetBillboard : MonoBehaviour
{
    Camera cam;

    void Awake() => cam = Camera.main;

    void LateUpdate()
    {
        if (!cam) return;
        // Face the camera while keeping an upright orientation (no roll)
        Vector3 lookPos = transform.position + cam.transform.rotation * Vector3.forward;
        Vector3 up = cam.transform.rotation * Vector3.up;
        transform.LookAt(lookPos, up);
    }
}
