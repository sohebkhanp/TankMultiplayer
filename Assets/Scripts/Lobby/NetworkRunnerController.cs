using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class NetworkRunnerController : MonoBehaviour, INetworkRunnerCallbacks
{
    public event Action OnStartedRunnerConnection;
    public event Action OnPlayerJoinedSuccessfully;

    [SerializeField] private NetworkRunner networkRunnerPrefab;
    private NetworkRunner networkRunnerInstance;

    public async void StartGame(GameMode gameMode, string roomName)
    {
        OnStartedRunnerConnection?.Invoke();

        if (networkRunnerInstance == null)
        {
            networkRunnerInstance = Instantiate(networkRunnerPrefab);
        }

        // Register callbacks
        networkRunnerInstance.AddCallbacks(this);

        networkRunnerInstance.ProvideInput = true;

        var startGameArgs = new StartGameArgs()
        {
            GameMode = gameMode,
            SessionName = roomName,
            PlayerCount = 4,
            SceneManager = networkRunnerInstance.GetComponent<INetworkSceneManager>()
        };

        var result = await networkRunnerInstance.StartGame(startGameArgs);

        if (result.Ok)
        {
            // great
            const string SCENE_NAME = "Game";
            await networkRunnerInstance.LoadScene(SCENE_NAME);
        }
        else
        {
            Debug.LogError($"Failed to start : {result.ShutdownReason}");
        }
    }

    public void ShutDownRunner()
    {
        networkRunnerInstance.Shutdown();
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) 
    { 
        Debug.Log("OnPlayerJoined");
        OnPlayerJoinedSuccessfully?.Invoke();
    }
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { Debug.Log("OnPlayerLeft"); }
    public void OnInput(NetworkRunner runner, NetworkInput input) { Debug.Log("OnInput"); }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { Debug.Log("OnInputMissing"); }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) 
    { 
        Debug.Log("OnShutdown");
        const string LOBBY_SCENE = "Lobby";
        SceneManager.LoadScene(LOBBY_SCENE);
    }
    public void OnConnectedToServer(NetworkRunner runner) { Debug.Log("OnConnectedToServer"); }
    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { Debug.Log("OnDisconnectedFromServer"); }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { Debug.Log("OnConnectRequest"); }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { Debug.Log("OnConnectFailed"); }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { Debug.Log("OnUserSimulationMessage"); }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { Debug.Log("OnSessionListUpdated"); }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { Debug.Log("OnCustomAuthenticationResponse"); }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { Debug.Log("OnHostMigration"); }
    public void OnSceneLoadDone(NetworkRunner runner) { Debug.Log("OnSceneLoadDone"); }
    public void OnSceneLoadStart(NetworkRunner runner) { Debug.Log("OnSceneLoadStart"); }
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { Debug.Log("OnObjectExitAOI"); }
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { Debug.Log("OnObjectEnterAOI"); }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { Debug.Log("OnReliableDataReceived"); }
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { Debug.Log("OnReliableDataProgress"); }

}
