using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Vector3 offset;
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Called after Update() call
    // Mostly used with Camera Gameobject
    private void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
