using UnityEngine;
public abstract class BaseInputHandler : MonoBehaviour
{
    public abstract Vector2 ControlInput { get; set; }
    public abstract float RollInput { get; set; }
    public abstract bool IsBoosting { get; set; }

    public abstract bool IsRightThrustDown { get; set; }
    public abstract bool IsLeftThrustDown { get; set; }
    public abstract bool BackFlipInput { get; set; }

    public virtual void UpdateInputs() { }

    public abstract bool IsRollInputDown { get; set; }
    public abstract int RollDirection { get; set; }

    public abstract bool IsBraking { get; set; }
}