using Fusion;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawnerController : NetworkBehaviour, IPlayerJoined, IPlayerLeft
{
    [SerializeField] private NetworkPrefabRef playerNetworkPrefab = NetworkPrefabRef.Empty;
    [SerializeField] private Transform[] spawnPoints;
    public override void Spawned()
    {
        //if (Runner.IsServer)
        //{
            //foreach (var item in Runner.ActivePlayers)
            //{
            //    SpawnPlayer(item);
            //}
        //}
    }

    private void SpawnPlayer(PlayerRef playerRef)
    {
        if (Runner.IsServer)
        {
            int index = playerRef.AsIndex % spawnPoints.Length;

            Vector3 spawnPoint = spawnPoints[index].transform.position;
            NetworkObject playerObject = Runner.Spawn(playerNetworkPrefab, spawnPoint, Quaternion.identity, playerRef);
            Runner.SetPlayerObject(playerRef, playerObject);

            Debug.LogWarning($"State Authority: {Object.HasStateAuthority}, Input Authority: {Object.HasInputAuthority}");

        }
    }

    public void PlayerJoined(PlayerRef player)
    {
        SpawnPlayer(player);
    }

    private void DespawnPlayer(PlayerRef playerRef)
    {
        if (Runner.IsServer)
        {
            if (Runner.TryGetPlayerObject(playerRef, out var playerNetworkObject))
            {
                Runner.Despawn(playerNetworkObject);
            }

            // Reset player object
            Runner.SetPlayerObject(playerRef, null);
        }
    }
    public void PlayerLeft(PlayerRef player)
    {
        DespawnPlayer(player);
    }
}
