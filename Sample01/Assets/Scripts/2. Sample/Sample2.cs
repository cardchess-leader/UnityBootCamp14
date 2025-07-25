using UnityEngine;

public enum Projection
{
    Perspective,
    Orthographic
}

public enum FieldOfViewAxis
{
    Horizontal,
    Vertical
}

public class Sample2 : MonoBehaviour
{
    public Projection projection;
    public FieldOfViewAxis fieldOfViewAxis;
    public float fieldOfView = 60f;
    public float near;
    public float far;
    public bool physicalCamera;


    // Update is called once per frame
    void Update()
    {
        
    }
}
