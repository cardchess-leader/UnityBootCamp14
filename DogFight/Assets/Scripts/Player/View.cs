using UnityEngine;
using Cinemachine;

public class View : MonoBehaviour
{
    [Header("Camera Reference")]
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private bool usePOVComponent = true; // POV ������Ʈ ��� ����
    
    [Header("Look Target (Alternative Method)")]
    [SerializeField] private Transform lookTarget; // ī�޶� �ٶ� Ÿ�� (POV ��� ���)
    [SerializeField] private float lookTargetDistance = 10f; // Ÿ�ٱ����� �Ÿ�
    
    [Header("Angles (degrees)")]
    public float maxYaw = 30f;    // left/right around Y
    public float maxPitch = 15f;  // up/down around X

    [Header("Smoothing")]
    [Tooltip("Seconds to reach ~63% of target. Lower = snappier")]
    public float smoothTime = 0.08f;

    [Header("Input shaping")]
    [Tooltip("Ignore tiny mouse offsets (0..1 in screen-normalized units)")]
    public float deadzone = 0.03f;
    [Tooltip("Clamp radial extent from center (0..1). 1 = screen edge")]
    public float clampRadius = 1f;

    private CinemachinePOV povComponent;
    private Vector2 currentInput;
    private Vector2 inputVelocity;
    private Vector3 baseLookDirection;

    void Start()
    {
        if (virtualCamera == null)
        {
            virtualCamera = FindFirstObjectByType<CinemachineVirtualCamera>();
        }

        if (usePOVComponent && virtualCamera != null)
        {
            // POV ������Ʈ�� �������ų� �߰�
            povComponent = virtualCamera.GetCinemachineComponent<CinemachinePOV>();
            if (povComponent == null)
            {
                povComponent = virtualCamera.AddCinemachineComponent<CinemachinePOV>();
            }
            
            // POV ����
            povComponent.m_HorizontalAxis.m_MaxSpeed = 0f; // ���� ����
            povComponent.m_VerticalAxis.m_MaxSpeed = 0f;   // ���� ����
            povComponent.m_HorizontalAxis.m_MinValue = -maxYaw;
            povComponent.m_HorizontalAxis.m_MaxValue = maxYaw;
            povComponent.m_VerticalAxis.m_MinValue = -maxPitch;
            povComponent.m_VerticalAxis.m_MaxValue = maxPitch;
        }
        else if (!usePOVComponent && lookTarget != null)
        {
            // Look Target ��� �ʱ�ȭ
            baseLookDirection = (lookTarget.position - transform.position).normalized;
        }
    }

    void LateUpdate()
    {
        // ���콺 �Է� ���
        Vector2 mouseInput = GetNormalizedMouseInput();
        
        // �ε巯�� �Է� ó��
        currentInput = Vector2.SmoothDamp(currentInput, mouseInput, ref inputVelocity, smoothTime);

        // ���õ� ����� ���� ī�޶� ����
        if (usePOVComponent && povComponent != null)
        {
            UpdateWithPOV();
        }
        else if (!usePOVComponent && lookTarget != null)
        {
            UpdateWithLookTarget();
        }
    }

    private Vector2 GetNormalizedMouseInput()
    {
        // ���콺�� ȭ�� �߾� �������� ����ȭ [-1, 1]
        Vector2 center = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        Vector2 half = center;
        Vector2 mPos = (Vector2)Input.mousePosition;
        Vector2 n = new Vector2(
            half.x > 0 ? (mPos.x - center.x) / half.x : 0f,
            half.y > 0 ? (mPos.y - center.y) / half.y : 0f
        );

        // ������ ����
        float r = n.magnitude;
        if (r < deadzone) n = Vector2.zero;
        else n *= (r - deadzone) / Mathf.Max(1e-5f, (1f - deadzone));

        // ������ Ŭ����
        n = Vector2.ClampMagnitude(n, Mathf.Clamp01(clampRadius));

        return n;
    }

    private void UpdateWithPOV()
    {
        // POV ������Ʈ�� ����� ī�޶� ����
        float targetYaw = currentInput.x * maxYaw;
        float targetPitch = -currentInput.y * maxPitch; // Y�� ����
        
        povComponent.m_HorizontalAxis.Value = targetYaw;
        povComponent.m_VerticalAxis.Value = targetPitch;
    }

    private void UpdateWithLookTarget()
    {
        // Look Target�� �̿��� ī�޶� ����
        float targetYaw = currentInput.x * maxYaw;
        float targetPitch = -currentInput.y * maxPitch;
        
        // �⺻ ���⿡�� ������ ����
        Quaternion yawRotation = Quaternion.AngleAxis(targetYaw, Vector3.up);
        Quaternion pitchRotation = Quaternion.AngleAxis(targetPitch, Vector3.right);
        
        Vector3 newLookDirection = yawRotation * pitchRotation * baseLookDirection;
        
        // Look Target ��ġ ������Ʈ
        lookTarget.position = transform.position + newLookDirection * lookTargetDistance;
        
        // Virtual Camera�� Look Target�� �ٶ󺸵��� ���� (�̹� �����Ǿ� �־�� ��)
    }

    void OnValidate()
    {
        // Inspector���� �� ���� �� POV ���� ������Ʈ
        if (povComponent != null && usePOVComponent)
        {
            povComponent.m_HorizontalAxis.m_MinValue = -maxYaw;
            povComponent.m_HorizontalAxis.m_MaxValue = maxYaw;
            povComponent.m_VerticalAxis.m_MinValue = -maxPitch;
            povComponent.m_VerticalAxis.m_MaxValue = maxPitch;
        }
    }
}
