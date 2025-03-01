using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level 2"); // Replace with Level 1 once created
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
