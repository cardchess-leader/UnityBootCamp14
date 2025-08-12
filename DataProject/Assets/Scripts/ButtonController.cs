using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void ReplayGame()
    {
        if (Time.timeScale != 0)
        {
            return;
        }
        SceneManager.LoadScene("PlayerPrefTaskScene"); // Load the Game Over scene
    }

    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false; // Stop playing in the editor
    }
}
