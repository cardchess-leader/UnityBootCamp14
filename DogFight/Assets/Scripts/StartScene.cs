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

        // ���콺 Ŭ��, Ű���� �Է�, �Ǵ� ��ġ �Է� ����
        if (Input.GetMouseButtonDown(0) || Input.anyKeyDown || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Real Scene");
        }
    }
}
