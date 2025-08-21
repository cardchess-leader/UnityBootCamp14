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
        rb.AddForce(LiftMagnitude() * liftForce * Time.deltaTime, ForceMode.Force);
    }

    Vector3 LiftMagnitude()
    {
        var diff = GameManager.Instance.thresholdAltitude - GameManager.Instance.GetAltitude(gameObject);
        //var dir = Mathf.Sign(diff);

        //var diff2 = ClampOutsidePoint1(1 / diff);
        return new Vector3(0, Mathf.Sign(diff), 0);
    }

    //float ClampOutsidePoint1(float value)
    //{
    //    return Mathf.Max(Mathf.Abs(value), 0.1f) * Mathf.Sign(value);
    //}
}