using UnityEngine;
public class KeyboardInputHandler : BaseInputHandler
{
    [SerializeField] private KeyCode boostKey = KeyCode.Space;

    [SerializeField] private KeyCode rollKeyLeft = KeyCode.Z;
    [SerializeField] private KeyCode rollKeyRight = KeyCode.C;

    [SerializeField] private KeyCode thrustKeyLeft = KeyCode.Q;
    [SerializeField] private KeyCode thrustKeyRight = KeyCode.E;

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

        if (Input.GetKeyDown(thrustKeyLeft))
        {
            IsLeftThrustDown = Input.GetKeyDown(thrustKeyLeft);
        }

        if (Input.GetKeyDown(thrustKeyRight))
        {
            IsRightThrustDown = Input.GetKeyDown(thrustKeyRight);
        }

        IsBoosting = Input.GetKey(boostKey);
        Debug.Log("Input IsBoosting: " + IsBoosting);

        BackFlipInput = Input.GetKeyDown(KeyCode.F);
    }
}