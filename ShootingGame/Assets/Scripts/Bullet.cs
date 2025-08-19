using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Vector3.up; // Assuming the bullet moves upwards
        transform.position += dir * speed * Time.deltaTime;
    }
}
