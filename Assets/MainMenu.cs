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
        #else
            // Exit in built file
            Application.Quit();
        #endif
    }
}
