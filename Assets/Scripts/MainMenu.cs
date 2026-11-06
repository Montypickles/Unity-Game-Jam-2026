using Unity.Collections.Tests.CoreCLR.TestJobs;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Game State");
    }

    public void OpenSettings() {
        SceneManager.LoadScene("Settings State");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}