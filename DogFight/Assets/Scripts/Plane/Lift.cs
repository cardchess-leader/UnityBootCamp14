using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField]
    float liftForce = 10; // Force applied for lift
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.AddForce(LiftDirection() * liftForce * Time.deltaTime, ForceMode.VelocityChange);
    }

    Vector3 LiftDirection()
    {
        if (GameManager.Instance.GetAltitude(gameObject) > GameManager.Instance.thresholdAltitude)
        {
            return Vector3.down; // If the altitude is above the threshold, apply lift downwards
        }
        return Vector3.up; // Otherwise, apply lift upwards
    }
}
