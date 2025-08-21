using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

// Singleton GameManager class to manage the game state
public class GameManager : MonoBehaviour
{
    // Use singleton pattern
    public static GameManager Instance { get; private set; }
    public float thresholdAltitude = 50f;

    private void Awake()
    {
        // Ensure that there is only one instance of GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this instance across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    public float GetAltitude(GameObject target)
    {
        return target.transform.position.y - gameObject.transform.position.y;
    }
}
