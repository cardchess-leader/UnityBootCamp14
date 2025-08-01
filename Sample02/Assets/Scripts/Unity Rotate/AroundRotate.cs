using UnityEngine;

public class AroundRotate : MonoBehaviour
{
    public Transform pivot;
    public float speed = 10;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(pivot.position, Vector3.up, speed * Time.deltaTime);
    }
}
