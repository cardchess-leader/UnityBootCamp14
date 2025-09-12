using UnityEngine;
public class KeyboardInputHandler : BaseInputHandler
{
    [SerializeField] private KeyCode boostKey = KeyCode.Space;

    [SerializeField] private KeyCode rollKeyLeft = KeyCode.Z;
    [SerializeField] private KeyCode rollKeyRight = KeyCode.C;

    [SerializeField] private KeyCode thrustKeyLeft = KeyCode.Q;
    [SerializeField] private KeyCode thrustKeyRight = KeyCode.E;

    // 더블 클릭 감지를 위한 변수들
    [SerializeField] private float doubleClickTime = 0.3f; // 더블 클릭 간격 (초)
    
    // 좌측 입력 더블 클릭용 변수들
    private float lastLeftInputTime = -1f;
    private int leftInputClickCount = 0;
    
    // 우측 입력 더블 클릭용 변수들
    private float lastRightInputTime = -1f;
    private int rightInputClickCount = 0;

    // 이전 프레임의 수평 축 값을 추적
    private float prevHorizontalInput = 0f;

    public override Vector2 ControlInput { get; set; }
    public override float RollInput { get; set; }
    public override bool IsBoosting { get; set; }

    public override bool IsRollInputDown { get; set; }
    public override int RollDirection { get; set; }

    public override bool IsRightThrustDown { get; set; }

    public override bool IsLeftThrustDown { get; set; }
    public override bool BackFlipInput { get; set; }

    public override void UpdateInputs()
    {
        ControlInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetKeyDown(rollKeyLeft))
        {
            IsRollInputDown = true;
            RollDirection = -1;
        }
        else if (Input.GetKeyDown(rollKeyRight))
        {
            IsRollInputDown = true;
            RollDirection = 1;
        }
        else
        {
            IsRollInputDown = false;
        }

        if (Input.GetKey(rollKeyLeft))
        {
            RollInput = -1f;
        }
        else if (Input.GetKey(rollKeyRight))
        {
            RollInput = 1f;
        }
        else
        {
            RollInput = 0f;
        }

        // 좌/우 수평 축 입력 더블 클릭 감지
        HandleHorizontalAxisDoubleClick();

        IsBoosting = Input.GetKey(boostKey);

        BackFlipInput = Input.GetKeyDown(KeyCode.F);
        
        // 현재 프레임의 수평 입력을 다음 프레임을 위해 저장
        prevHorizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void HandleHorizontalAxisDoubleClick()
    {
        float currentHorizontalInput = Input.GetAxisRaw("Horizontal"); // -1, 0, 1의 값

        // 좌측 입력 감지 (0에서 -1로 변화)
        if (currentHorizontalInput < 0 && prevHorizontalInput >= 0)
        {
            IsLeftThrustDown = HandleDoubleClick(ref lastLeftInputTime, ref leftInputClickCount, "Left");
        }

        // 우측 입력 감지 (0에서 1로 변화)  
        if (currentHorizontalInput > 0 && prevHorizontalInput <= 0)
        {
            IsRightThrustDown = HandleDoubleClick(ref lastRightInputTime, ref rightInputClickCount, "Right");
        }

        // 시간 초과 시 초기화
        IsLeftThrustDown = ResetIfTimeExpired(ref lastLeftInputTime, ref leftInputClickCount, IsLeftThrustDown);
        IsRightThrustDown = ResetIfTimeExpired(ref lastRightInputTime, ref rightInputClickCount, IsRightThrustDown);
    }

    private bool HandleDoubleClick(ref float lastInputTime, ref int clickCount, string direction)
    {
        float currentTime = Time.time;
        
        // 첫 번째 클릭이거나 너무 오래된 클릭인 경우
        if (lastInputTime == -1f || currentTime - lastInputTime > doubleClickTime)
        {
            clickCount = 1;
            lastInputTime = currentTime;
            return false;
        }
        // 더블 클릭 시간 내의 두 번째 클릭인 경우
        else if (clickCount == 1 && currentTime - lastInputTime <= doubleClickTime)
        {
            clickCount = 2;
            Debug.Log($"Double click detected on {direction} input!");
            return true;
        }
        
        return false;
    }

    private bool ResetIfTimeExpired(ref float lastInputTime, ref int clickCount, bool currentThrustDown)
    {
        // 더블 클릭 시간이 지나면 초기화
        if (lastInputTime != -1f && Time.time - lastInputTime > doubleClickTime)
        {
            if (clickCount < 2)
            {
                clickCount = 0;
                lastInputTime = -1f;
                return false;
            }
        }
        
        return currentThrustDown;
    }
}