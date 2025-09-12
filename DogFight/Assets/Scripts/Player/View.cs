using UnityEngine;
using Cinemachine;

public class View : MonoBehaviour
{
    [Header("Camera Reference")]
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private bool usePOVComponent = true; // POV 컴포넌트 사용 여부
    
    [Header("Look Target (Alternative Method)")]
    [SerializeField] private Transform lookTarget; // 카메라가 바라볼 타겟 (POV 대신 사용)
    [SerializeField] private float lookTargetDistance = 10f; // 타겟까지의 거리
    
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
            // POV 컴포넌트를 가져오거나 추가
            povComponent = virtualCamera.GetCinemachineComponent<CinemachinePOV>();
            if (povComponent == null)
            {
                povComponent = virtualCamera.AddCinemachineComponent<CinemachinePOV>();
            }
            
            // POV 설정
            povComponent.m_HorizontalAxis.m_MaxSpeed = 0f; // 수동 제어
            povComponent.m_VerticalAxis.m_MaxSpeed = 0f;   // 수동 제어
            povComponent.m_HorizontalAxis.m_MinValue = -maxYaw;
            povComponent.m_HorizontalAxis.m_MaxValue = maxYaw;
            povComponent.m_VerticalAxis.m_MinValue = -maxPitch;
            povComponent.m_VerticalAxis.m_MaxValue = maxPitch;
        }
        else if (!usePOVComponent && lookTarget != null)
        {
            // Look Target 방식 초기화
            baseLookDirection = (lookTarget.position - transform.position).normalized;
        }
    }

    void LateUpdate()
    {
        // 마우스 입력 계산
        Vector2 mouseInput = GetNormalizedMouseInput();
        
        // 부드러운 입력 처리
        currentInput = Vector2.SmoothDamp(currentInput, mouseInput, ref inputVelocity, smoothTime);

        // 선택된 방법에 따라 카메라 제어
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
        // 마우스를 화면 중앙 기준으로 정규화 [-1, 1]
        Vector2 center = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        Vector2 half = center;
        Vector2 mPos = (Vector2)Input.mousePosition;
        Vector2 n = new Vector2(
            half.x > 0 ? (mPos.x - center.x) / half.x : 0f,
            half.y > 0 ? (mPos.y - center.y) / half.y : 0f
        );

        // 데드존 적용
        float r = n.magnitude;
        if (r < deadzone) n = Vector2.zero;
        else n *= (r - deadzone) / Mathf.Max(1e-5f, (1f - deadzone));

        // 반지름 클램프
        n = Vector2.ClampMagnitude(n, Mathf.Clamp01(clampRadius));

        return n;
    }

    private void UpdateWithPOV()
    {
        // POV 컴포넌트를 사용한 카메라 제어
        float targetYaw = currentInput.x * maxYaw;
        float targetPitch = -currentInput.y * maxPitch; // Y축 반전
        
        povComponent.m_HorizontalAxis.Value = targetYaw;
        povComponent.m_VerticalAxis.Value = targetPitch;
    }

    private void UpdateWithLookTarget()
    {
        // Look Target을 이용한 카메라 제어
        float targetYaw = currentInput.x * maxYaw;
        float targetPitch = -currentInput.y * maxPitch;
        
        // 기본 방향에서 오프셋 적용
        Quaternion yawRotation = Quaternion.AngleAxis(targetYaw, Vector3.up);
        Quaternion pitchRotation = Quaternion.AngleAxis(targetPitch, Vector3.right);
        
        Vector3 newLookDirection = yawRotation * pitchRotation * baseLookDirection;
        
        // Look Target 위치 업데이트
        lookTarget.position = transform.position + newLookDirection * lookTargetDistance;
        
        // Virtual Camera가 Look Target을 바라보도록 설정 (이미 설정되어 있어야 함)
    }

    void OnValidate()
    {
        // Inspector에서 값 변경 시 POV 설정 업데이트
        if (povComponent != null && usePOVComponent)
        {
            povComponent.m_HorizontalAxis.m_MinValue = -maxYaw;
            povComponent.m_HorizontalAxis.m_MaxValue = maxYaw;
            povComponent.m_VerticalAxis.m_MinValue = -maxPitch;
            povComponent.m_VerticalAxis.m_MaxValue = maxPitch;
        }
    }
}
