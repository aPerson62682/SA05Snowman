using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject pausedText;


    public void RestartPurlyScene()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        AudioManager.instance.PauseMusic();
        AudioManager.instance.PlayMusic("IntroTheme");


    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartPurlyScene();
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        pausedText.SetActive(true);
        AudioManager.instance.PauseMusic();
        AudioManager.instance.PlayMusic("IntroTheme");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausedText.SetActive(false);
        AudioManager.instance.PlayMusic("MainTheme");
    }
}