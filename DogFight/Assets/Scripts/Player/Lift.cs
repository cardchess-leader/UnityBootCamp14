using System.Collections;
using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField]
    float liftForce = 10; // Force applied for lift
    Rigidbody rb;
    [SerializeField]
    float counterGravityForce = 0.9f; // in sacle of 0 ~ 1.0
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ApplyZoneLift();
    }

    void ApplyZoneLift()
    {
        //var liftDirection = new Vector3(0, 1, 0);
        //if (transform.position.y < GameManager.Instance.liftUpBelowThisAltitude)
        //{
        //    rb.AddForce(liftDirection * liftForce * Time.deltaTime, ForceMode.Force);
        //} else
        //{
        //    // Add default gravityscale to offset the gravity effect (by that much fraction)
        //    rb.AddForce(liftDirection * counterGravityForce, ForceMode.Force);
        //}
    }
}