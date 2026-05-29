using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public static class ScoreSyncHandler
{
    private const string ScoreTopic = "DualSingleplayerScore";
    private static bool isRegistered;

    public static void Register()
    {
        var manager = NetworkManager.Singleton;
        if (!DualSingleplayerSession.IsActive || manager == null || !manager.IsListening || isRegistered)
            return;

        manager.CustomMessagingManager.RegisterNamedMessageHandler(ScoreTopic, OnReceiveScore);
        isRegistered = true;
    }

    public static void Unregister()
    {
        var manager = NetworkManager.Singleton;
        if (manager == null || !isRegistered)
            return;

        manager.CustomMessagingManager.UnregisterNamedMessageHandler(ScoreTopic);
        isRegistered = false;
    }

    public static void SendScore(int score)
    {
        var manager = NetworkManager.Singleton;
        if (!DualSingleplayerSession.IsActive || manager == null || !manager.IsListening)
            return;

        Register();

        using var writer = new FastBufferWriter(sizeof(int), Allocator.Temp);
        writer.WriteValueSafe(score);

        if (manager.IsServer)
            manager.CustomMessagingManager.SendNamedMessageToAll(ScoreTopic, writer);
        else
            manager.CustomMessagingManager.SendNamedMessage(ScoreTopic, NetworkManager.ServerClientId, writer);
    }

    private static void OnReceiveScore(ulong senderId, FastBufferReader reader)
    {
        if (!reader.TryBeginRead(sizeof(int)))
            return;

        reader.ReadValueSafe(out int remoteScore);

        if (NetworkManager.Singleton != null && senderId == NetworkManager.Singleton.LocalClientId)
            return;

        if (PlayerScore.Instance != null)
            PlayerScore.Instance.SetRemoteScore(remoteScore);
    }
}
