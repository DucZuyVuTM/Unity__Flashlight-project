using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    const string FinalScoreKey = "FinalScore";
    const string FinalScoreRivalKey = "FinalScoreRival";
    const string FinalWasMultiplayerKey = "FinalWasMultiplayer";

    public Text scoreText;

    void Start()
    {
        int finalScore = PlayerPrefs.GetInt(FinalScoreKey, 0);
        int rivalScore = PlayerPrefs.GetInt(FinalScoreRivalKey, 0);
        bool wasMultiplayer = PlayerPrefs.GetInt(FinalWasMultiplayerKey, 0) == 1;

        if (scoreText == null)
            return;

        if (wasMultiplayer)
        {
            scoreText.fontSize = 30;
            scoreText.text = $"You: {finalScore}\nRival: {rivalScore}";
        }
        else
        {
            scoreText.fontSize = 60;
            scoreText.text = finalScore.ToString();
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
