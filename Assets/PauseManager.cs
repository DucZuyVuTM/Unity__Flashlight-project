using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;
    public Text pauseButtonText;
    public Text stopButtonText;
    private bool isPaused = false;
    private float lastClickTime = 0f;
    private float clickCooldown = 0.2f;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        if (pauseButtonText != null)
            pauseButtonText.text = "⏸";
        if (stopButtonText != null)
            stopButtonText.text = "■";
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.qKey.wasPressedThisFrame)
            TogglePause();
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
            Stop();
    }

    public void TogglePause()
    {
        if (Time.unscaledTime - lastClickTime < clickCooldown) return;
        lastClickTime = Time.unscaledTime;

        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;

        if (pauseButtonText != null)
            pauseButtonText.text = isPaused ? "▶" : "⏸";
    }

    public void Stop()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("FinalScore", 0);
        SceneManager.LoadScene("MainMenu");
    }

    void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}
