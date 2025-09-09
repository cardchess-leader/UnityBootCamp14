using Cinemachine;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private CinemachineVirtualCamera crashCamera;

    BasePlaneController paperPlanePhysics;

    private void Awake()
    {
        paperPlanePhysics = FindFirstObjectByType<BasePlaneController>();
    }

    private void OnEnable()
    {
        paperPlanePhysics.OnCrash += HandleCrash;
        paperPlanePhysics.OnReset += HandleReset;
        paperPlanePhysics.OnBackFlipStart += HandleStunt;

        paperPlanePhysics.OnBackFlipEnd += EndStunt;
    }


    private void OnDisable()
    {
        paperPlanePhysics.OnCrash -= HandleCrash;
        paperPlanePhysics.OnReset -= HandleReset;

        paperPlanePhysics.OnBackFlipStart -= HandleStunt;
        paperPlanePhysics.OnBackFlipEnd -= EndStunt;
    }

    private void HandleReset()
    {
        crashCamera.Priority = 0;
        cinemachineVirtualCamera.Priority = 15;
        paperPlanePhysics.EnableEffects();
    }

    private void HandleCrash()
    {
        crashCamera.Priority = 15;
        paperPlanePhysics.DisableEffects();
    }


    private void HandleStunt()
    {
        cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_BindingMode =
            CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp;
    }

    private void EndStunt()
    {
        cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_BindingMode =
            CinemachineTransposer.BindingMode.LockToTargetNoRoll;
    }
}