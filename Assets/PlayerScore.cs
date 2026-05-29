using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    const int MaxScore = 1000000000;
    const string FinalScoreKey = "FinalScore";
    const string FinalScoreRivalKey = "FinalScoreRival";
    const string FinalWasMultiplayerKey = "FinalWasMultiplayer";

    public static PlayerScore Instance;
    public Text scoreText;
    public Text rivalScoreText;
    private int score = 0;
    private int remoteScore = 0;

    public int LocalScore => score;
    public int RemoteScore => remoteScore;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        score = 0;
        remoteScore = 0;
        SaveFinalScores();
        EnsureRivalScoreText();
        UpdateScoreUI();

        if (DualSingleplayerSession.IsActive)
        {
            ScoreSyncHandler.Register();
            ScoreSyncHandler.SendScore(score);
        }
    }

    public void AddScore(int amount)
    {
        score = Mathf.Min(MaxScore, score + amount);
        SaveFinalScores();
        UpdateScoreUI();

        if (DualSingleplayerSession.IsActive)
            ScoreSyncHandler.SendScore(score);
    }

    public void SetRemoteScore(int value)
    {
        remoteScore = Mathf.Clamp(value, 0, MaxScore);
        SaveFinalScores();
        Debug.Log($"[ScoreSync] Remote score updated: {remoteScore}");
        UpdateScoreUI();
    }

    void SaveFinalScores()
    {
        PlayerPrefs.SetInt(FinalScoreKey, score);
        PlayerPrefs.SetInt(FinalScoreRivalKey, remoteScore);
        PlayerPrefs.SetInt(FinalWasMultiplayerKey, DualSingleplayerSession.IsActive ? 1 : 0);
    }

    void UpdateScoreUI()
    {
        if (scoreText == null)
            return;

        if (!DualSingleplayerSession.IsActive)
        {
            scoreText.text = score.ToString();
            if (rivalScoreText != null)
                rivalScoreText.gameObject.SetActive(false);
            return;
        }

        scoreText.text = $"You:\n{score}";
        if (rivalScoreText != null)
        {
            rivalScoreText.gameObject.SetActive(true);
            rivalScoreText.text = $"Rival:\n{remoteScore}";
        }
    }

    void EnsureRivalScoreText()
    {
        if (!DualSingleplayerSession.IsActive || scoreText == null || rivalScoreText != null)
            return;

        var rivalObject = new GameObject("RivalScoreText", typeof(RectTransform), typeof(Text));
        rivalObject.transform.SetParent(scoreText.transform.parent, false);

        var rect = rivalObject.GetComponent<RectTransform>();
        rect.anchorMin = scoreText.rectTransform.anchorMin;
        rect.anchorMax = scoreText.rectTransform.anchorMax;
        rect.pivot = scoreText.rectTransform.pivot;
        rect.sizeDelta = new Vector2(160f, scoreText.rectTransform.sizeDelta.y);
        rect.anchoredPosition = scoreText.rectTransform.anchoredPosition + new Vector2(-180f, 0f);

        var text = rivalObject.GetComponent<Text>();
        text.font = scoreText.font != null ? scoreText.font : Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        text.fontSize = scoreText.fontSize;
        text.alignment = TextAnchor.MiddleRight;
        text.color = new Color(1f, 0.45f, 0.1f);
        text.text = "Rival: 0";

        rivalScoreText = text;
    }
}
