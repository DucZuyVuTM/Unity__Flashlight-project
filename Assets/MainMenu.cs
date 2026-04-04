using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    // Update is called once per frame
    public void ExitGame()
    {
        Debug.Log("Выход из игры");
        
        #if UNITY_EDITOR
            // Exit in edit mode
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Exit in built file
            Application.Quit();
        #endif
    }
}
