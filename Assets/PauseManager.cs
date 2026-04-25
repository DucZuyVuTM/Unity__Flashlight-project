using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;
    public Text pauseButtonText;
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
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
            TogglePause();
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

    void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}
