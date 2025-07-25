using UnityEngine;

public class BoxRotate : MonoBehaviour
{
    public Vector3 pos;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(pos * Time.deltaTime);
    }
}
