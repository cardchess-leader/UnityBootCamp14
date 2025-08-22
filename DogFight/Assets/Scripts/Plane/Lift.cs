using System.Collections;
using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField]
    float liftForce = 10; // Force applied for lift
    Rigidbody rb;
    public float gravityScale = 490f;
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
        var direction = new Vector3(0, 0, 0);
        var magnitude = liftForce;
        if (transform.position.y < GameManager.Instance.liftUpBelowThisAltitude)
        {
            direction = new Vector3(0, 1, 0);
        }
        else if (transform.position.y > GameManager.Instance.liftDownBelowThisAltitude)
        {
            direction = new Vector3(0, -1, 0);
        } else
        {
            direction = new Vector3(0, 1, 0);
            magnitude = gravityScale;
        }
        rb.AddForce(direction * magnitude * Time.deltaTime, ForceMode.Force);
    }
}