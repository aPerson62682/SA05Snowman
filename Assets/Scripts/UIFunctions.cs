using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class UIFunctions : MonoBehaviour
{
    [SerializeField] private GameObject setNameMenu;
    [SerializeField] private GameObject HighScores;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private TextMeshProUGUI playerName;
    [SerializeField]
    private TMP_InputField inputField;

    public static class PlayerInfo
    {
        public static string playerName;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void getPlayerName()
    {
        string name = inputField.text;

        playerName.text = name; // For preview/display
        string formattedName = name.EndsWith("s") ? name + "'" : name + "'s";
        PlayerInfo.playerName = formattedName; // Store raw name for later use
    }


    public void newGame()
    {
        Time.timeScale = 1f;

        AudioManager.instance.PlayMusic("MainTheme");  // Switch music

        SceneManager.LoadScene("Adventure of Snowman 2d");
    }

    public void exitButton()
    {
        Application.Quit();
        Debug.Log("Game is exiting..."); // Helpful for testing in the editor
    }
    public void changeName()
    {
        setNameMenu.SetActive(true);
    }

    public void returnToMainMenu()
    {
        HighScores.SetActive(false);
        setNameMenu.SetActive(false);
    }
    public void viewHighScore()
    {
        HighScores.SetActive(true);
    }


}
