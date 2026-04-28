using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start button
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    // Exit button
    public void ExitGame()
    {
        Debug.Log("Exit game");
        
        #if UNITY_EDITOR
            // Exit in edit mode
            UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_WEBGL
            // Exit in WebGL - close browser tab
            Application.OpenURL("javascript:window.close()");
        #else
            // Exit in built file
            Application.Quit();
        #endif
    }
}
