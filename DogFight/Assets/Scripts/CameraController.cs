using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool isPOVMode = true;
    Camera povCamera;
    Cinemachine.CinemachineBrain cinemachineCamera;
    public Camera GetCamera()
    {
        // return povCamera when in POV mode
        if (isPOVMode && povCamera != null)
        {
            return povCamera;
        }
        // return cinemachine camera when not in POV mode
        if (cinemachineCamera != null)
        {
            return cinemachineCamera.GetComponent<Camera>();
        }
        return Camera.main;
    }

    public void TogglePOVMode()
    {
        isPOVMode = !isPOVMode;
        SetCamera();
    }

    private void Start()
    {
        povCamera = GameObject.FindWithTag("POVCamera").GetComponent<Camera>();
        cinemachineCamera = FindFirstObjectByType<Cinemachine.CinemachineBrain>();
        SetCamera();
    }

    private void SetCamera()
    {
        povCamera.gameObject.SetActive(isPOVMode);
        cinemachineCamera.gameObject.SetActive(!isPOVMode);
    }
}
