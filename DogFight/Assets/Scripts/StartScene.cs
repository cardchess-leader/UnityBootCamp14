using UnityEngine;

public class StartScene : MonoBehaviour
{
    [SerializeField]
    float timeScale = 0.1f;
    private void Start()
    {
        Time.timeScale = timeScale; // Ensure normal time scale at the start
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }

    private void Update()
    {
        // Rotate the object for a simple animation effect
        transform.Rotate(0, 20 * Time.deltaTime, 0);

        // 마우스 클릭, 키보드 입력, 또는 터치 입력 감지
        if (Input.GetMouseButtonDown(0) || Input.anyKeyDown || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Real Scene");
        }
    }
}
