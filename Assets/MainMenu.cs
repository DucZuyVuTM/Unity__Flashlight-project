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
            // Exit in WebGL - tell the player to close browser tab
            Application.OpenURL("javascript:alert('Thank you for playing! You can close the game tab manually.');");
        #else
            // Exit in built file
            Application.Quit();
        #endif
    }
}
