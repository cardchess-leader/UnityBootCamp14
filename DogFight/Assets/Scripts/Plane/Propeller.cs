using UnityEngine;

// This script is attached to a propeller GameObject in Unity.
// The purpose is to rotate the propeller constantly to simulate spinning.
public class Propeller : MonoBehaviour
{
    // Speed of the propeller rotation in degrees per second.
    public float rotationSpeed = 1f; // 1 Rounds per second
    // Update is called once per frame.
    void Update()
    {
        // Calculate the rotation angle for this frame.
        float rotationAngle = 360 * rotationSpeed * Time.deltaTime;
        // Apply the rotation to the propeller GameObject.
        transform.Rotate(Vector3.forward, rotationAngle);
    }
}
