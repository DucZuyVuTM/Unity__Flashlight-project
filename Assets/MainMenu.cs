using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Multiplayer")]
    [SerializeField] private NetworkLauncher networkLauncher;

    private void Awake()
    {
        if (networkLauncher == null)
            networkLauncher = GetComponent<NetworkLauncher>();
    }

    // Start button
    public void StartGame()
    {
        DualSingleplayerSession.Disable();
        ScoreSyncHandler.Unregister();
        SceneManager.LoadScene("Game");
    }

    public void StartAsHost()
    {
        if (networkLauncher == null)
        {
            Debug.LogWarning("[MainMenu] Missing NetworkLauncher component.");
            return;
        }

        networkLauncher.StartAsHost();
    }

    public void StartAsClient()
    {
        if (networkLauncher == null)
        {
            Debug.LogWarning("[MainMenu] Missing NetworkLauncher component.");
            return;
        }

        networkLauncher.StartAsClientWithFallback();
    }

    // Exit button
    public void ExitGame()
    {
        Debug.Log("Exit game");
        
        #if UNITY_EDITOR
            // Exit in edit mode
            UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_WEBGL
            // Exit in WebGL - tell the player to close browser tab
            Application.OpenURL("javascript:alert('Thank you for playing! You can close the game tab manually.');");
        #else
            // Exit in built file
            Application.Quit();
        #endif
    }
}
