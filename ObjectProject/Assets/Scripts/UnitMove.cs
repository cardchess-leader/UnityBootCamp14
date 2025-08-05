using UnityEngine;

public class UnitMove : MonoBehaviour
{
    Vector3 initPos;
    float time = 0;
    private void Start()
    {
        initPos = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime * 0.1f; // Adjust the speed of movement by changing the multiplier
        transform.position = initPos + initPos * time;
    }
}
