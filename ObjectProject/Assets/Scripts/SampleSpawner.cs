using UnityEngine;

public class SampleSpawner : MonoBehaviour
{
    void Start()
    {
        GameObject sample = GameObject.Find("Sample");

        if (sample == null)
        {
            sample = new GameObject("Sample");
            sample.AddComponent<Sample>();
        }
        else
        {
            Debug.Log("Sample 오브젝트가 존재합니다.");
        }
    }
}
