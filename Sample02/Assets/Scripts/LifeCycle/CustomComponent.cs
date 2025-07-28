using UnityEngine;

public class CustomComponent : MonoBehaviour
{
    public string message;
    private void Awake()
    {
        Debug.Log("[CustomComponent Awake] " + message);
    }
}

// Edit -> Project Settings -> Script Execution Order