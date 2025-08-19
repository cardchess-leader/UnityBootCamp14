using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5;
    float h, v;

    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, v, 0);

        transform.position += dir * speed * Time.deltaTime;
    }
}
