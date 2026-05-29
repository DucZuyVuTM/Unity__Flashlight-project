using System.Collections;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkLauncher : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "Game";
    [SerializeField] private string hostAddress = "127.0.0.1";
    [SerializeField] private ushort port = 7777;
    [SerializeField] private bool useWebSockets = true;
    [SerializeField] private float connectionTimeout = 3f;

    public void StartAsClientWithFallback()
    {
        StartCoroutine(TryConnectAsClient());
    }

    private IEnumerator TryConnectAsClient()
    {
        bool connected = false;
        float startTime = Time.time;

        System.Action<ulong> onConnected = (id) =>
        {
            if (id == NetworkManager.Singleton.LocalClientId)
            {
                connected = true;
                Debug.Log("[NetworkLauncher] Successfully connected to Host!");
            }
        };

        NetworkManager.Singleton.OnClientConnectedCallback += onConnected;

        ConfigureTransport(NetworkManager.Singleton, hostAddress, port, useWebSockets);

        if (!NetworkManager.Singleton.StartClient())
        {
            Debug.LogError("[NetworkLauncher] Failed to start client.");
            NetworkManager.Singleton.OnClientConnectedCallback -= onConnected;
            yield break;
        }

        yield return new WaitUntil(() => connected || Time.time - startTime >= connectionTimeout);

        NetworkManager.Singleton.OnClientConnectedCallback -= onConnected;

        if (connected) yield break;

        Debug.Log("[NetworkLauncher] Timeout: No host found. Starting as Host.");
        NetworkManager.Singleton.Shutdown();

        yield return new WaitUntil(() => !NetworkManager.Singleton.IsListening);

        StartAsHost();
    }

    public void StartAsHost()
    {
        var manager = NetworkManager.Singleton;
        if (manager == null)
        {
            Debug.LogError("[NetworkLauncher] NetworkManager.Singleton is null.");
            return;
        }

        if (manager.IsListening)
        {
            if (manager.IsHost)
            {
                Debug.LogWarning("[NetworkLauncher] Already running as Host.");
                return;
            }
            manager.Shutdown();
        }

        ConfigureTransport(manager, hostAddress, port, useWebSockets);

        if (manager.StartHost())
        {
            DualSingleplayerSession.Enable();
            ScoreSyncHandler.Register();
            manager.SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);
        }
        else
            Debug.LogError("[NetworkLauncher] Failed to start host.");
    }

    public void StartAsClient()
    {
        var manager = NetworkManager.Singleton;
        if (manager == null)
        {
            Debug.LogError("[NetworkLauncher] NetworkManager.Singleton is null.");
            return;
        }

        if (manager.IsListening)
        {
            if (manager.IsClient && !manager.IsServer)
            {
                Debug.LogWarning("[NetworkLauncher] Already running as Client.");
                return;
            }
            manager.Shutdown();
        }

        ConfigureTransport(manager, hostAddress, port, useWebSockets);

        if (manager.StartClient())
        {
            DualSingleplayerSession.Enable();
            ScoreSyncHandler.Register();
        }
        else
            Debug.LogError("[NetworkLauncher] Failed to start client.");
    }

    private static void ConfigureTransport(NetworkManager manager, string address, ushort connectionPort, bool enableWebSockets)
    {
        var transport = manager.GetComponent<UnityTransport>();
        if (transport != null)
        {
            transport.SetConnectionData(address, connectionPort);
            transport.UseWebSockets = enableWebSockets;
        }
        else
            Debug.LogError("[NetworkLauncher] UnityTransport component is missing on NetworkManager.");
    }
}
