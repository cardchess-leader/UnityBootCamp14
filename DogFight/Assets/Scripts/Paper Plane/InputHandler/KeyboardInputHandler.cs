using UnityEngine;
public class KeyboardInputHandler : BaseInputHandler
{
    [SerializeField] private KeyCode boostKey = KeyCode.Space;

    [SerializeField] private KeyCode rollKeyLeft = KeyCode.Z;
    [SerializeField] private KeyCode rollKeyRight = KeyCode.C;

    [SerializeField] private KeyCode thrustKeyLeft = KeyCode.Q;
    [SerializeField] private KeyCode thrustKeyRight = KeyCode.E;

    // ���� Ŭ�� ������ ���� ������
    [SerializeField] private float doubleClickTime = 0.3f; // ���� Ŭ�� ���� (��)
    
    // ���� �Է� ���� Ŭ���� ������
    private float lastLeftInputTime = -1f;
    private int leftInputClickCount = 0;
    
    // ���� �Է� ���� Ŭ���� ������
    private float lastRightInputTime = -1f;
    private int rightInputClickCount = 0;

    // ���� �������� ���� �� ���� ����
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

        // ��/�� ���� �� �Է� ���� Ŭ�� ����
        HandleHorizontalAxisDoubleClick();

        IsBoosting = Input.GetKey(boostKey);

        BackFlipInput = Input.GetKeyDown(KeyCode.F);
        
        // ���� �������� ���� �Է��� ���� �������� ���� ����
        prevHorizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void HandleHorizontalAxisDoubleClick()
    {
        float currentHorizontalInput = Input.GetAxisRaw("Horizontal"); // -1, 0, 1�� ��

        // ���� �Է� ���� (0���� -1�� ��ȭ)
        if (currentHorizontalInput < 0 && prevHorizontalInput >= 0)
        {
            IsLeftThrustDown = HandleDoubleClick(ref lastLeftInputTime, ref leftInputClickCount, "Left");
        }

        // ���� �Է� ���� (0���� 1�� ��ȭ)  
        if (currentHorizontalInput > 0 && prevHorizontalInput <= 0)
        {
            IsRightThrustDown = HandleDoubleClick(ref lastRightInputTime, ref rightInputClickCount, "Right");
        }

        // �ð� �ʰ� �� �ʱ�ȭ
        IsLeftThrustDown = ResetIfTimeExpired(ref lastLeftInputTime, ref leftInputClickCount, IsLeftThrustDown);
        IsRightThrustDown = ResetIfTimeExpired(ref lastRightInputTime, ref rightInputClickCount, IsRightThrustDown);
    }

    private bool HandleDoubleClick(ref float lastInputTime, ref int clickCount, string direction)
    {
        float currentTime = Time.time;
        
        // ù ��° Ŭ���̰ų� �ʹ� ������ Ŭ���� ���
        if (lastInputTime == -1f || currentTime - lastInputTime > doubleClickTime)
        {
            clickCount = 1;
            lastInputTime = currentTime;
            return false;
        }
        // ���� Ŭ�� �ð� ���� �� ��° Ŭ���� ���
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
        // ���� Ŭ�� �ð��� ������ �ʱ�ȭ
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