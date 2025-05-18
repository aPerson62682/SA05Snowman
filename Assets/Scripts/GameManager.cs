using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject gameOverScreen;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ShowGameOver()
    {
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
            Time.timeScale = 0f;
            AudioManager.instance.PauseMusic();
            AudioManager.instance.PlaySFX("GameOverTheme");
        }
        else
        {
            Debug.LogWarning("GameOverScreen is not assigned in GameManager!");
        }
    }
}
