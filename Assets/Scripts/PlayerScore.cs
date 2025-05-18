using static UIFunctions;
using TMPro;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public static PlayerScore instance;

    [SerializeField] private TextMeshProUGUI scoreText;

    private int score = 0;
    private string playerName;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        playerName = PlayerInfo.playerName;
        if (string.IsNullOrEmpty(playerName))
        {
            playerName = "Player's";
        }
        UpdateScoreText(); // Show initial score
    }

    // New version supports positive or negative points
    public void AddPoint(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = $"{playerName} Score: {score}";
    }
}
