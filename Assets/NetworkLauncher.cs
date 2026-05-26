using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkLauncher : MonoBehaviour
{    
    [SerializeField] private string gameSceneName = "Multiplayer";

    public void StartAsHost()
    {
        
        if (NetworkManager.Singleton.StartHost())
        {
            
            NetworkManager.Singleton.SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);
        }
    }

    public void StartAsClient()
    {
        
        NetworkManager.Singleton.StartClient();
    }
}
